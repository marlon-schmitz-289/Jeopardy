import { createRouter, createWebHashHistory, type RouteRecordRaw } from 'vue-router'
import GameView from '@/views/GameView.vue'
import EditorView from '@/views/EditorView.vue'
import SettingsView from '@/views/SettingsView.vue'

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        redirect: '/game'
    },
    {
        path: '/game',
        name: 'Game',
        component: GameView
    },
    {
        path: '/editor',
        name: 'Editor',
        component: EditorView
    },
    {
        path: '/settings',
        name: 'Settings',
        component: SettingsView
    }
]

const router = createRouter({
    history: createWebHashHistory(),
    routes
})

export default router