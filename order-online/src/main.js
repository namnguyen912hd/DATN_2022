import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import Toast from "vue-toastification";
// Import the CSS or use your own!
import "vue-toastification/dist/index.css";
import * as VueGoogleMaps from "vue2-google-maps";
import VueGeolocation from 'vue-browser-geolocation';

Vue.use(require('vue-shortkey'))
Vue.use(VueGeolocation);
Vue.use(VueGoogleMaps, {
  load: {
    key: "AIzaSyDZUQU9n9ryJymq61m7IFloSiXbKPLe-i4",
    libraries: "places"
  }
});
Vue.config.productionTip = false
Vue.use(Toast, {
  transition: "Vue-Toastification__fade",
  maxToasts: 20,
  newestOnTop: true
});
new Vue({
  Toast,
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
