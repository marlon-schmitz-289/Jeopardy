import { createApp } from 'vue'
import { createPinia } from 'pinia'
import type { App } from 'vue'

import AppComponent from './App.vue'
import router from './router'

// Import global styles
import './assets/main.css'

const app: App = createApp(AppComponent)

app.use(createPinia())
app.use(router)

app.mount('#app')