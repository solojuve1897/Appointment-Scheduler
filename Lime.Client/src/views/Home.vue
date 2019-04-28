<template>
<div>
  <div class="teal">
    <v-layout align-center class="pa-2" wrap>
      <v-flex xs12>
      <v-autocomplete
        class="pa-2"
        prepend-inner-icon="search"
        dark
        v-model="select"
        solo-inverted
        hide-details
        flat
        multiple
        placeholder="Search employees..."
        :loading="searching"
        hide-no-data
        :items="employees"
        item-text="name"
        item-value="id"
        :search-input.sync="search" />
      </v-flex>
    </v-layout>
  </div>
  <v-layout class="pt-5" wrap>
    <v-flex md4 xs12>
        <v-menu
          ref="earliestDateMenu"
          v-model="earliestDateMenu"
          :close-on-content-click="false"
          :return-value.sync="earliestDate"
          lazy
          transition="scale-transition"
          offset-y
          full-width
        >
          <template v-slot:activator="{ on }">
            <v-text-field
              class="pa-3"
              v-model="earliestDate"
              label="Earliest Date"
              prepend-icon="event"
              readonly
              v-on="on"
            ></v-text-field>
          </template>
          <v-date-picker
            color="teal"
            v-model="earliestDate"
            no-title
            scrollable
            min="2015-01-01"
            max="2015-04-30">
            <v-spacer></v-spacer>
            <v-btn flat color="primary" @click="earliestDateMenu = false">Cancel</v-btn>
            <v-btn flat color="primary" @click="$refs.earliestDateMenu.save(earliestDate)">OK</v-btn>
          </v-date-picker>
        </v-menu>
      </v-flex>
      <v-flex md4 xs12>
        <v-menu
          ref="latestDateMenu"
          v-model="latestDateMenu"
          :close-on-content-click="false"
          :return-value.sync="latestDate"
          lazy
          transition="scale-transition"
          offset-y
          full-width
          min-width="290px"
        >
          <template v-slot:activator="{ on }">
            <v-text-field
              class="pa-3"
              v-model="latestDate"
              label="Latest Date"
              prepend-icon="event"
              readonly
              v-on="on"
            ></v-text-field>
          </template>
          <v-date-picker
            color="teal"
            v-model="latestDate"
            no-title
            scrollable
            min="2015-01-01"
            max="2015-04-30">
            <v-spacer></v-spacer>
            <v-btn flat color="primary" @click="latestDateMenu = false">Cancel</v-btn>
            <v-btn flat color="primary" @click="$refs.latestDateMenu.save(latestDate)">OK</v-btn>
          </v-date-picker>
        </v-menu>
      </v-flex>
      <v-flex md4 xs12>
        <v-select
          :items="meetingLengthItems"
          item-text="value"
          item-value="key"
          class="pa-3"
          v-model="meetingLength"
          label="Meeting Length"
          prepend-icon="timelapse"
        />
      </v-flex>
      <v-flex class="pt-3" md4 xs12>
        <v-menu
          ref="officeHourFromMenu"
          v-model="officeHourFromMenu"
          :close-on-content-click="false"
          :return-value.sync="officeHourFrom"
          lazy
          transition="scale-transition"
          offset-y
          full-width
          max-width="290px"
          min-width="290px"
        >
          <template v-slot:activator="{ on }">
            <v-text-field
              class="pa-3"
              v-model="officeHourFrom"
              label="Office Hour From"
              prepend-icon="access_time"
              readonly
              v-on="on"
            ></v-text-field>
          </template>
          <v-time-picker
            color="teal"
            v-if="officeHourFromMenu"
            v-model="officeHourFrom"
            format="24hr"
            min="7:00"
            max="17:00"
            :allowed-minutes="allowedMinutes"
            full-width
            @click:minute="$refs.officeHourFromMenu.save(officeHourFrom)"
          ></v-time-picker>
        </v-menu>
      </v-flex>
      <v-flex class="pt-3" md4 xs12>
        <v-menu
          ref="officeHourToMenu"
          v-model="officeHourToMenu"
          :close-on-content-click="false"
          :return-value.sync="officeHourTo"
          lazy
          transition="scale-transition"
          offset-y
          full-width
          max-width="290px"
          min-width="290px"
        >
          <template v-slot:activator="{ on }">
            <v-text-field
              class="pa-3"
              v-model="officeHourTo"
              label="Office Hour To"
              prepend-icon="access_time"
              readonly
              v-on="on"
            ></v-text-field>
          </template>
          <v-time-picker
            color="teal"
            v-if="officeHourToMenu"
            v-model="officeHourTo"
            format="24hr"
            min="7:00"
            max="17:00"
            :allowed-minutes="allowedMinutes"
            full-width
            @click:minute="$refs.officeHourToMenu.save(officeHourTo)"
          ></v-time-picker>
        </v-menu>
      </v-flex>
      <v-flex class="pt-3" xs12>
        <v-layout>
          <v-spacer></v-spacer>
          <v-btn color="primary" round large @click="clear">Clear</v-btn>
          <v-btn color="teal" :dark="isValid" round large :disabled="!isValid" @click="submit" :loading="loading">Submit</v-btn>
        </v-layout>
      </v-flex>
  </v-layout>
  <v-dialog lazy max-width="290" v-model="error">
    <v-card>
      <v-card-text class="text-xs-center">
        <v-icon color="red" size="56">error</v-icon>
      </v-card-text>
      <v-card-text class="subheading text-xs-center pt-0">
        An unexpected error occured. Please try again.
      </v-card-text>
      <v-card-actions>
        <v-spacer/>
        <v-btn flat color="primary" @click="error = false">Ok</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <suggestions-dialog :suggestions="suggestions" :dialog="showSuggestions" @close="showSuggestions = false"/>
</div>
</template>

<script>
import { mapActions } from 'vuex'
import SuggestionsDialog from '@/components/SuggestionsDialog'
export default {
  components: {
    SuggestionsDialog
  },
  data () {
    return {
      select: [],
      search: null,
      employees: [],
      suggestions: [],
      showSuggestions: false,
      searching: false,
      earliestDate: '2015-01-01',
      earliestDateMenu: false,
      latestDate: '2015-04-30',
      latestDateMenu: false,
      meetingLengthItems: [{ key: 30, value: '30 minutes' }, { key: 60, value: '1 hour' }, { key: 90, value: '1 hour 30 minutes' }, { key: 120, value: '2 hours' }],
      meetingLength: 60,
      officeHourFromMenu: false,
      officeHourFrom: '08:00',
      officeHourToMenu: false,
      officeHourTo: '17:00',
      loading: false,
      error: false
    }
  },
  watch: {
    search (val) {
      val && val.length > 1 && val && this.fetchEmployees(val)
    }
  },
  methods: {
    ...mapActions('employee', ['fetch']),
    ...mapActions('suggestion', ['getSuggestions']),
    fetchEmployees (val) {
      this.searching = true
      this.fetch(val)
        .then((result) => {
          this.employees = result.data.matches
          this.searching = false
        }).catch(() => {
          this.searching = false
          this.error = true
        })
    },
    allowedMinutes: v => v === 30 || v === 0,
    clear () {
      this.earliestDate = '2015-01-01'
      this.latestDate = '2015-04-30'
      this.meetingLength = 60
      this.officeHourFrom = '08:00'
      this.officeHourTo = '17:00'
      this.select = []
    },
    submit () {
      this.loading = true
      this.getSuggestions({ employees: this.select, fromDate: this.earliestDate, toDate: this.latestDate, officeHoursStart: this.officeHourFrom, officeHoursEnd: this.officeHourTo, meetingLength: this.meetingLength })
        .then((result) => {
          this.loading = false
          this.suggestions = result.data.suggestions
          this.showSuggestions = true
        }).catch(() => {
          this.suggestions = []
          this.loading = false
          this.error = true
        })
    }
  },
  computed: {
    isValid () {
      return this.select.length > 0
    }
  }
}
</script>

<style lang="scss">
.search-icon{
  width: 0px;
}
</style>
