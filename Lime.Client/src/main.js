import Vue from 'vue'
import '@/plugins/vuetify'
import '@/plugins/axios'
import interceptorsSetup from '@/extensions/interceptors'
import App from '@/App'
import router from '@/router'
import store from '@/store'
import 'roboto-fontface/css/roboto/roboto-fontface.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'

Vue.config.productionTip = false
interceptorsSetup()

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
