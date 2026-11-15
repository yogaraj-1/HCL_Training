# Yogaraj P — Developer Portfolio (Angular 22)

Requires Node 22.22.3+ or 24.15+ (check with `node -v`).

    npm install
    npm start        # http://localhost:4200
    npm run build    # production build in dist/

Edit your content in `src/app/core/data/portfolio-data.ts` (projects, skills, experience, education, certifications)
and `src/app/core/data/social-links.ts`.

Skill levels, the AgriConnect / Student Result / Hospital projects and the stats on Home are placeholders
from the original prompt: keep them accurate before sharing.

To go live with a backend: set `useBackend = true` in `portfolio.service.ts` and `contact.service.ts`.
