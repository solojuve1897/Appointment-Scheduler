using System;
using System.Collections.Generic;
using System.Linq;
using Lime.Common;
using Lime.Data;
using Lime.Domain;

namespace Lime.Business
{
    public class SuggestionLogic : ISuggestionLogic
    {
        private readonly IBusyTimeRepository _busyTimeRepository;
        private const int BusyTimeIdentifier = 15;
        private const int AppointmentTimeInterval = 30;

        public SuggestionLogic(IBusyTimeRepository busyTimeRepository)
        {
            _busyTimeRepository = busyTimeRepository;
        }

        /// <summary>
        /// Returns a suggestions-list with all bookable times and dates
        /// </summary>
        public IList<SuggestionDto> GetSuggestions(SuggestionsQueryParams parameters)
        {
            var fromDate = DateTimeOffset.Parse($"{parameters.FromDate} {parameters.OfficehoursStart}");
            var toDate = DateTimeOffset.Parse($"{parameters.ToDate} {parameters.OfficehoursEnd}");

            var desiredSuggestions = InitializeSuggestions(parameters, fromDate, toDate);
            var availableSuggestions = GetAvailableSuggestions(desiredSuggestions, parameters, fromDate, toDate);

            return availableSuggestions;
        }

        /// <summary>
        /// Creates and returns a list with all desired time-bookings according to the inputs made by the user
        /// </summary>
        private List<SuggestionDto> InitializeSuggestions(SuggestionsQueryParams parameters, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var suggestions = new List<SuggestionDto>();

            while (fromDate <= toDate)
            {
                var suggestion = new SuggestionDto { Date = fromDate.GetShortDate() };
                var officeHoursStart = DateTimeOffset.Parse($"{fromDate.GetShortDate()} {parameters.OfficehoursStart}");
                var officeHoursEnds = DateTimeOffset.Parse($"{fromDate.GetShortDate()} {parameters.OfficehoursEnd}").AddMinutes(-parameters.MeetingLength);

                while (officeHoursStart <= officeHoursEnds)
                {
                    suggestion.StartTimes.Add(officeHoursStart.GetTime());
                    officeHoursStart = officeHoursStart.AddMinutes(AppointmentTimeInterval);
                }

                suggestions.Add(suggestion);
                fromDate = fromDate.AddDays(1);
            }

            return suggestions;
        }

        /// <summary>
        /// Returns a list with all the times which are available for booking
        /// </summary>
        private List<SuggestionDto> GetAvailableSuggestions(List<SuggestionDto> suggestions, 
                                                            SuggestionsQueryParams parameters, 
                                                            DateTimeOffset fromDate, 
                                                            DateTimeOffset toDate)
        {
            var busyTimesForEmployees = _busyTimeRepository.GetBusyTimes(parameters.Employees)
                                                           .Where(x => x.StartTime >= fromDate && x.EndTime <= toDate)
                                                           .OrderBy(x => x.StartTime)
                                                           .ToList();

            var busyTimes = GetBusyTimesOrderedList(busyTimesForEmployees);

            foreach (var suggestion in suggestions)
            {
                // if there is no busy times on this particular date then skip it, i.e. keep desired times as is
                if (!busyTimesForEmployees.Any(x => x.StartTime.GetShortDate() == suggestion.Date))
                    continue;

                var availableStartTimes = new List<string>();
                var officeHoursEnds = DateTimeOffset.Parse($"{suggestion.Date} {parameters.OfficehoursEnd}");

                foreach (var startTime in suggestion.StartTimes)
                {
                    var startTimeDate = DateTimeOffset.Parse($"{suggestion.Date} {startTime}");
                    var isAvailable = false;

                    for (int i = 0; i < busyTimes.Count; i++)
                    {
                        // Check if current startTime, while considering meetingLength, is available when it's before earliest busyTime.
                        if (i == 0 && busyTimes[i] >= startTimeDate.AddMinutes(parameters.MeetingLength))
                        {
                            isAvailable = true;
                            break;
                        }

                        // On the last busyTime we want to check if it's still within the office hour
                        // while taking into consideration the meetinglength
                        if (i == busyTimes.Count - 1)
                        {
                            if (busyTimes[i].AddMinutes(parameters.MeetingLength) <= officeHoursEnds && 
                                startTimeDate >= busyTimes[i])
                            {
                                isAvailable = true;
                                break;
                            }
                        }
                        // Check if we have available time between the current and the next busyTimes
                        else if ((busyTimes[i + 1] - busyTimes[i]).TotalMinutes != BusyTimeIdentifier &&
                                 startTimeDate >= busyTimes[i] &&
                                 startTimeDate.AddMinutes(parameters.MeetingLength) <= busyTimes[i + 1])
                        {
                            isAvailable = true;
                            break;
                        }
                    }

                    if (isAvailable)
                        availableStartTimes.Add(startTimeDate.GetTime());
                }

                suggestion.StartTimes = availableStartTimes;
            }

            return suggestions;
        }

        /// <summary>
        /// Returns an ordered flat list of the times in busyTimesForEmployees plus filler-times based on
        /// "BusyTimeIdentifier" (which is set to 15 minutes). When two items of time in
        /// the list follows each other and has more than 15 minutes between them that time in
        /// question is then available to book.
        /// Example; [0] = 09:00, [1] = 10:00. The time between 09:00 and 10:00 is bookable.
        /// Example; [0] = 10:00, [1] = 10:15. The time between 10:00 and 10:15 is not bookable.
        /// This also means that the system can only check availability for meeting-length equal to
        /// or above 30 minutes.
        /// </summary>
        private List<DateTimeOffset> GetBusyTimesOrderedList(List<BusyTime> busyTimesForEmployees)
        {
            var busyTimes = new List<DateTimeOffset>();

            foreach (var busyTime in busyTimesForEmployees)
            {
                var startTime = busyTime.StartTime;
                var endTime = busyTime.EndTime;

                while (startTime <= endTime)
                {
                    busyTimes.Add(startTime);
                    startTime = startTime.AddMinutes(BusyTimeIdentifier);
                }
            }

            return busyTimes.Distinct().OrderBy(x => x).ToList();
        }
    }
}
