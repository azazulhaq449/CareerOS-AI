import { useScrollPosition } from '../../hooks/useScrollPosition'

const VISIBLE_THRESHOLD = 500

export function BackToTop() {
  const scrollY = useScrollPosition()
  const isVisible = scrollY > VISIBLE_THRESHOLD

  return (
    <a
      href="#home"
      onClick={(e) => {
        e.preventDefault()
        window.scrollTo({ top: 0, behavior: 'smooth' })
      }}
      id="back-to-top"
      className="back-to-top fs-5"
      style={{ display: isVisible ? 'block' : 'none' }}
    >
      <i className="uil uil-arrow-up"></i>
    </a>
  )
}
