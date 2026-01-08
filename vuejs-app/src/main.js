/**
 * @file main.js
 * @description Application entry point for the CPMS Vue.js application.
 * Initializes Vue app with router, Vuex store, Vuetify UI framework, and web fonts.
 */

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'

// Load Google Fonts (Roboto) for Vuetify
loadFonts()

// Create and mount the Vue application with all plugins
createApp(App)
  .use(router)
  .use(store)
  .use(vuetify)
  .mount('#app')
