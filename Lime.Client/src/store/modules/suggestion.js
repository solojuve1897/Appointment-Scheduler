import Vue from 'vue'
let axios = Vue.axios

const state = {
}

const getters = {
}

const actions = {
  getSuggestions ({ commit }, params) {
    return getSuggestions(params).then((result) => {
      if (result.status === 200) {
        return result
      }
    }).catch((error) => {
      return Promise.reject(error)
    })
  }
}

const mutations = {
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}

// Api methods
const getSuggestions = (params) => {
  var parameters = new URLSearchParams()
  for (var prop in params) {
    if (prop === 'employees') {
      params[prop].forEach(employee => {
        parameters.append(prop, employee)
      })
    } else {
      parameters.append(prop, params[prop])
    }
  }
  return axios.get('suggestions', {
    params: parameters
  })
}
