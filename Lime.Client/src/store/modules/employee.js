import Vue from 'vue'
let axios = Vue.axios

const state = {
}

const getters = {
}

const actions = {
  fetch ({ commit }, val) {
    return fetch(val).then(result => {
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
const fetch = (val) => {
  return axios.get('employees', {
    params: {
      q: val
    }
  })
}
