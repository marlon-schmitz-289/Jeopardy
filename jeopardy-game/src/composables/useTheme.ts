import { ref, computed } from 'vue'

type ThemeMode = 'light' | 'dark' | 'system'

function loadMode(): ThemeMode {
  const saved = localStorage.getItem('theme-mode')
  if (saved === 'light' || saved === 'dark' || saved === 'system') return saved
  // backward compat: old key was 'theme'
  if (localStorage.getItem('theme') === 'dark') return 'dark'
  return 'light'
}

const themeMode = ref<ThemeMode>(loadMode())
const systemDark = ref(window.matchMedia('(prefers-color-scheme: dark)').matches)

window.matchMedia('(prefers-color-scheme: dark)')
  .addEventListener('change', e => { systemDark.value = e.matches; syncTheme() })

const isDark = computed(() =>
  themeMode.value === 'dark' || (themeMode.value === 'system' && systemDark.value)
)

function syncTheme() {
  document.documentElement.dataset.theme = isDark.value ? 'dark' : 'light'
}

function setThemeMode(mode: ThemeMode) {
  themeMode.value = mode
  localStorage.setItem('theme-mode', mode)
  syncTheme()
}

// backward compat for GameNavbar toggle button
function toggleTheme() {
  setThemeMode(isDark.value ? 'light' : 'dark')
}

export function useTheme() {
  return { themeMode, isDark, setThemeMode, syncTheme, toggleTheme }
}
