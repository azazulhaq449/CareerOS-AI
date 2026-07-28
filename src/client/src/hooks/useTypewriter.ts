import { useEffect, useState } from 'react'

const TYPE_SPEED_MS = 70
const DELETE_SPEED_MS = 40
const HOLD_MS = 1800

/** Cycles through `words`, typing and deleting each one in turn. */
export function useTypewriter(words: string[]): string {
  const [wordIndex, setWordIndex] = useState(0)
  const [text, setText] = useState('')
  const [isDeleting, setIsDeleting] = useState(false)

  useEffect(() => {
    const currentWord = words[wordIndex % words.length]

    if (!isDeleting && text === currentWord) {
      const holdTimer = setTimeout(() => setIsDeleting(true), HOLD_MS)
      return () => clearTimeout(holdTimer)
    }

    if (isDeleting && text === '') {
      setIsDeleting(false)
      setWordIndex((i) => i + 1)
      return
    }

    const step = setTimeout(
      () => {
        setText((current) =>
          isDeleting ? current.slice(0, -1) : currentWord.slice(0, current.length + 1),
        )
      },
      isDeleting ? DELETE_SPEED_MS : TYPE_SPEED_MS,
    )
    return () => clearTimeout(step)
  }, [text, isDeleting, wordIndex, words])

  return text
}
