import { defineStore } from 'pinia'
import { ref, reactive } from 'vue'

export interface ColorSet {
  accent: string
  bgStart: string
  bgEnd: string
  card: string
}

type ThemeMode = 'light' | 'dark' | 'system'

export const COLOR_DEFAULTS = {
  light: { accent: '#c34d7a', bgStart: '#ffffff', bgEnd: '#ff86bd', card: '#f0f0f0' } as ColorSet,
  dark:  { accent: '#7c3aed', bgStart: '#0f172a', bgEnd: '#1a0a3c', card: '#1e293b' } as ColorSet,
}

function loadSettings(): { themeMode: ThemeMode; lightColors: ColorSet; darkColors: ColorSet } {
  try {
    const saved = localStorage.getItem('jeopardy-settings')
    if (saved) {
      const parsed = JSON.parse(saved)
      return {
        themeMode: (parsed.themeMode || 'light') as ThemeMode,
        lightColors: { ...COLOR_DEFAULTS.light, ...parsed.lightColors },
        darkColors:  { ...COLOR_DEFAULTS.dark,  ...parsed.darkColors  },
      }
    }
  } catch {}
  return {
    themeMode: 'light',
    lightColors: { ...COLOR_DEFAULTS.light },
    darkColors:  { ...COLOR_DEFAULTS.dark  },
  }
}

export const useSettingsStore = defineStore('settings', () => {
  const saved = loadSettings()
  const themeMode    = ref<ThemeMode>(saved.themeMode)
  const lightColors  = reactive<ColorSet>(saved.lightColors)
  const darkColors   = reactive<ColorSet>(saved.darkColors)

  function applyColors() {
    const el = document.documentElement
    el.style.setProperty('--lt-accent',   lightColors.accent)
    el.style.setProperty('--lt-bg-start', lightColors.bgStart)
    el.style.setProperty('--lt-bg-end',   lightColors.bgEnd)
    el.style.setProperty('--lt-card',     lightColors.card)
    el.style.setProperty('--dk-accent',   darkColors.accent)
    el.style.setProperty('--dk-bg-start', darkColors.bgStart)
    el.style.setProperty('--dk-bg-end',   darkColors.bgEnd)
    el.style.setProperty('--dk-card',     darkColors.card)
  }

  function save() {
    localStorage.setItem('jeopardy-settings', JSON.stringify({
      themeMode: themeMode.value,
      lightColors: { ...lightColors },
      darkColors:  { ...darkColors  },
    }))
  }

  function setThemeMode(mode: ThemeMode) {
    themeMode.value = mode
    save()
  }

  function updateLightColor(key: keyof ColorSet, value: string) {
    lightColors[key] = value
    applyColors()
    save()
  }

  function updateDarkColor(key: keyof ColorSet, value: string) {
    darkColors[key] = value
    applyColors()
    save()
  }

  function resetLight() {
    Object.assign(lightColors, COLOR_DEFAULTS.light)
    applyColors()
    save()
  }

  function resetDark() {
    Object.assign(darkColors, COLOR_DEFAULTS.dark)
    applyColors()
    save()
  }

  return {
    themeMode, lightColors, darkColors,
    applyColors, setThemeMode,
    updateLightColor, updateDarkColor,
    resetLight, resetDark,
  }
})
