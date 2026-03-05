export enum QuestionType {
    Text = 'text',
    Audio = 'audio',
    Image = 'image',
    YouTube = 'youtube'
}

export interface Question {
    id: number
    points: number
    question: string
    answer: string
    type: QuestionType
    used: boolean
}

export interface Category {
    id: number
    name: string
    questions: Question[]
}

export interface GameState {
    categories: Category[]
    players: string[]
    currentQuestion: Question | null
    scores: Record<string, number>
}

export interface QuestionSelection {
    categoryId: number
    questionId: number
    question: Question
}

// YouTube utilities
export interface YouTubePlayerOptions {
    height: string
    width: string
    videoId: string
    playerVars?: {
        autoplay?: 0 | 1
        controls?: 0 | 1
        start?: number
        length?: number
        loop?: 0 | 1
        mute?: 0 | 1
        rel?: 0 | 1
    }
}

export const extractYouTubeVideoId = (url: string): string | null => {
    const patterns = [
        /(?:youtube\.com\/watch\?v=|youtu\.be\/|youtube\.com\/embed\/)([^&\n?#]+)/,
        /^([a-zA-Z0-9_-]{11})$/ // Direct video ID
    ]

    for (const pattern of patterns) {
        const match = url.match(pattern)
        if (match) {
            return match[1]
        }
    }

    return null
}