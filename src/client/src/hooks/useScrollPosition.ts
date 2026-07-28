import { useEffect, useState } from 'react'

/** Tracks the current vertical scroll offset of the window. */
export function useScrollPosition(): number {
  const [scrollY, setScrollY] = useState(() => window.scrollY)

  useEffect(() => {
    const handleScroll = () => setScrollY(window.scrollY)
    window.addEventListener('scroll', handleScroll, { passive: true })
    return () => window.removeEventListener('scroll', handleScroll)
  }, [])

  return scrollY
}
