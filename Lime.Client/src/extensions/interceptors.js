import Vue from 'vue'

export default function setup () {
  Vue.axios.interceptors.request.use(
    function (config) {
      // Do something before request is sent
      return config
    },
    function (error) {
      // Do something with request error
      return Promise.reject(error)
    }
  )

  // Add a response interceptor
  Vue.axios.interceptors.response.use(
    function (response) {
      // Do something with response data
      return response
    },
    function (error) {
      // Do something with response error
      return Promise.reject(error)
    }
  )
}
