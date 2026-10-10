# Sweet Crumbs Bakery — Bootstrap 5 website

A responsive, elegant cream/pink/brown bakery website with four pages:
- `index.html` — home page
- `menu.html` — sample product menu
- `about.html` — bakery story
- `custom-order.html` — custom cake request form

## Run locally
1. Extract `sweet-crumbs-bakery.zip`.
2. Open the extracted folder in VS Code.
3. Install the **Live Server** extension (if needed).
4. Right-click `index.html` → **Open with Live Server**.
5. Use the navigation menu to open the other pages.

Internet is required for the Bootstrap CDN, Google Fonts and online Unsplash photos.

## Personalize the website
- **Bakery name and navigation:** edit `js/main.js` and replace `Sweet Crumbs`.
- **Homepage text:** edit `index.html`.
- **Menu items and prices:** edit `menu.html` and the favourite cards in `index.html`.
- **Colors, fonts and layout:** edit CSS variables at the top of `css/style.css` (`--cream`, `--pink`, `--brown`).
- **Images:** replace each `src="https://images.unsplash.com/..."` URL with your own image URL, or save your photos under `images/` and use paths such as `images/chocolate-cake.jpg`.
- **Contact details:** update the address and phone placeholders in the footer template in `js/main.js`.

## Important: order form
The form currently validates entries and displays a request summary in the browser. It does **not** send, store, or notify anyone about the order. To receive real orders, connect it to a backend/API or a form service, and add privacy and data-handling information. Do not collect customer details until the submission destination is configured.

## Before publishing
- Replace sample prices, products, opening hours and contact details with accurate information.
- Check image usage/licensing and use your own product photographs where possible.
- Test all links and the form on desktop and mobile.
- Publish through a static host such as GitHub Pages, Netlify or Vercel once customized.
