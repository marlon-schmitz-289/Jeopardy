import { defineStore } from 'pinia'
import type { Category, Question, QuestionType } from '@/types/game'

interface GameStore {
    categories: Category[]
    players: string[]
    currentQuestion: Question | null
    scores: Record<string, number>
}

export const useGameStore = defineStore('game', {
    state: (): GameStore => ({
        categories: Array(6).fill(null).map((_, i): Category => ({
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
        })),
        players: [],
        currentQuestion: null,
        scores: {}
    }),

    actions: {
        setPlayers(players: string[]): void {
            this.players = players
            this.scores = players.reduce((acc: Record<string, number>, player: string) => {
                acc[player] = 0
                return acc
            }, {})
        },

        addPlayer(playerName: string): void {
            if (!this.players.includes(playerName) && this.players.length < 6) {
                this.players.push(playerName)
                this.scores[playerName] = 0
            }
        },

        removePlayer(playerName: string): void {
            const index = this.players.indexOf(playerName)
            if (index > -1 && this.players.length > 1) {
                this.players.splice(index, 1)
                delete this.scores[playerName]
            }
        },

        updateScore(player: string, points: number): void {
            if (this.scores[player] !== undefined) {
                this.scores[player] += points
            }
        },

        markQuestionUsed(categoryId: number, questionId: number): void {
            const category = this.categories[categoryId]
            if (category && category.questions[questionId]) {
                category.questions[questionId].used = true
            }
        },

        updateCategory(categoryId: number, name: string): void {
            if (this.categories[categoryId]) {
                this.categories[categoryId].name = name
            }
        },

        updateQuestion(categoryId: number, questionId: number, questionData: Partial<Question>): void {
            const question = this.categories[categoryId]?.questions[questionId]
            if (question) {
                Object.assign(question, questionData)
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
        }
    }
})