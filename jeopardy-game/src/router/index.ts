import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import GameView from '@/views/GameView.vue'
import EditorView from '@/views/EditorView.vue'

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
    }
]

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes
})

export default router