import { createApp } from 'vue'
import { createPinia } from 'pinia'
import type { App } from 'vue'
import AppComponent from './App.vue'
import router from './router'
import './assets/main.css'
import { useSettingsStore } from './stores/settingsStore'
import { useTheme } from './composables/useTheme'

const app: App = createApp(AppComponent)

const pinia = createPinia()
app.use(pinia)
app.use(router)

// Apply persisted settings before mounting to prevent flash
const settings = useSettingsStore()
settings.applyColors()
useTheme().setThemeMode(settings.themeMode as 'light' | 'dark' | 'system')

app.mount('#app')
