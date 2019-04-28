import Vue from 'vue'
import Vuex from 'vuex'
import app from '@/store/modules/app'
import employee from '@/store/modules/employee'
import suggestion from '@/store/modules/suggestion'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
  },
  modules: {
    app,
    employee,
    suggestion
  }
})
