import { defineStore } from 'pinia'
import type { Category, Question, QuestionType } from '@/types/game'

const PLAYERS_STORAGE_KEY = 'jeopardy-players'
const CATEGORIES_STORAGE_KEY = 'jeopardy-categories'
const SCORES_STORAGE_KEY = 'jeopardy-scores'

function loadFromStorage<T>(key: string): T | null {
    try {
        const stored = localStorage.getItem(key)
        if (stored) {
            return JSON.parse(stored)
        }
    } catch {
        // Ignore storage errors
    }
    return null
}

function saveToStorage(key: string, data: unknown): void {
    try {
        localStorage.setItem(key, JSON.stringify(data))
    } catch {
        // Ignore storage errors
    }
}

function loadPlayersFromStorage(): string[] {
    const players = loadFromStorage<string[]>(PLAYERS_STORAGE_KEY)
    if (Array.isArray(players) && players.every(p => typeof p === 'string')) {
        return players
    }
    return []
}

function createDefaultCategories(): Category[] {
    return Array(6).fill(null).map((_, i): Category => ({
        id: i,
        name: `Category ${i + 1}`,
        questions: Array(5).fill(null).map((_, j): Question => ({
            id: j,
            points: (j + 1) * 200,
            question: '',
            answer: '',
            type: 'text' as QuestionType,
            used: false
        }))
    }))
}

function loadCategoriesFromStorage(): Category[] {
    const categories = loadFromStorage<Category[]>(CATEGORIES_STORAGE_KEY)
    if (Array.isArray(categories) && categories.length === 6) {
        return categories
    }
    return createDefaultCategories()
}

function loadScoresFromStorage(players: string[]): Record<string, number> {
    const scores = loadFromStorage<Record<string, number>>(SCORES_STORAGE_KEY)
    if (scores && typeof scores === 'object') {
        // Ensure all current players have scores
        const validScores: Record<string, number> = {}
        for (const player of players) {
            validScores[player] = typeof scores[player] === 'number' ? scores[player] : 0
        }
        return validScores
    }
    return players.reduce((acc: Record<string, number>, player: string) => {
        acc[player] = 0
        return acc
    }, {})
}

interface GameStore {
    categories: Category[]
    players: string[]
    currentQuestion: Question | null
    scores: Record<string, number>
}

const savedPlayers = loadPlayersFromStorage()
const savedCategories = loadCategoriesFromStorage()
const savedScores = loadScoresFromStorage(savedPlayers)

export const useGameStore = defineStore('game', {
    state: (): GameStore => ({
        categories: savedCategories,
        players: savedPlayers,
        currentQuestion: null,
        scores: savedScores
    }),

    actions: {
        setPlayers(players: string[]): void {
            this.players = players
            this.scores = players.reduce((acc: Record<string, number>, player: string) => {
                acc[player] = 0
                return acc
            }, {})
            saveToStorage(PLAYERS_STORAGE_KEY, this.players)
            saveToStorage(SCORES_STORAGE_KEY, this.scores)
        },

        addPlayer(playerName: string): void {
            if (!this.players.includes(playerName) && this.players.length < 6) {
                this.players.push(playerName)
                this.scores[playerName] = 0
                saveToStorage(PLAYERS_STORAGE_KEY, this.players)
                saveToStorage(SCORES_STORAGE_KEY, this.scores)
            }
        },

        removePlayer(playerName: string): void {
            const index = this.players.indexOf(playerName)
            if (index > -1 && this.players.length > 1) {
                this.players.splice(index, 1)
                delete this.scores[playerName]
                saveToStorage(PLAYERS_STORAGE_KEY, this.players)
                saveToStorage(SCORES_STORAGE_KEY, this.scores)
            }
        },

        updateScore(player: string, points: number): void {
            if (this.scores[player] !== undefined) {
                this.scores[player] += points
                saveToStorage(SCORES_STORAGE_KEY, this.scores)
            }
        },

        markQuestionUsed(categoryId: number, questionId: number): void {
            const category = this.categories[categoryId]
            if (category && category.questions[questionId]) {
                category.questions[questionId].used = true
                saveToStorage(CATEGORIES_STORAGE_KEY, this.categories)
            }
        },

        updateCategory(categoryId: number, name: string): void {
            if (this.categories[categoryId]) {
                this.categories[categoryId].name = name
                saveToStorage(CATEGORIES_STORAGE_KEY, this.categories)
            }
        },

        updateQuestion(categoryId: number, questionId: number, questionData: Partial<Question>): void {
            const question = this.categories[categoryId]?.questions[questionId]
            if (question) {
                Object.assign(question, questionData)
                saveToStorage(CATEGORIES_STORAGE_KEY, this.categories)
            }
        },

        resetGame(): void {
            this.categories.forEach(category => {
                category.questions.forEach(question => {
                    question.used = false
                })
            })

            Object.keys(this.scores).forEach(player => {
                this.scores[player] = 0
            })

            saveToStorage(CATEGORIES_STORAGE_KEY, this.categories)
            saveToStorage(SCORES_STORAGE_KEY, this.scores)
        },

        resetBoard(): void {
            this.categories = createDefaultCategories()
            saveToStorage(CATEGORIES_STORAGE_KEY, this.categories)
        }
    }
})