// Shared header and footer keep all pages consistent.
const currentPage = window.location.pathname.split("/").pop() || "index.html";
const navItems = [
  ["index.html", "Home"],
  ["menu.html", "Our Menu"],
  ["about.html", "Our Story"],
  ["custom-order.html", "Custom Cake Order"]
];

const header = document.getElementById("site-header");
if (header) {
  header.innerHTML = `
    <nav class="navbar navbar-expand-lg bg-white sticky-top shadow-sm">
      <div class="container">
        <a class="navbar-brand fw-bold" href="index.html">♡ Sweet Crumbs</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#mainNav" aria-controls="mainNav" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="mainNav">
          <ul class="navbar-nav ms-auto align-items-lg-center">
            ${navItems.map(([href, label]) => `<li class="nav-item"><a class="nav-link ${currentPage === href ? "active" : ""}" ${currentPage === href ? 'aria-current="page"' : ""} href="${href}">${label}</a></li>`).join("")}
          </ul>
        </div>
      </div>
    </nav>`;
}

const footer = document.getElementById("site-footer");
if (footer) {
  footer.innerHTML = `
    <footer class="site-footer">
      <div class="container">
        <div class="row g-4">
          <div class="col-md-6"><div class="footer-brand">♡ Sweet Crumbs Bakery</div><p class="mt-2 mb-0">Thoughtful bakes for life's sweetest moments.</p></div>
          <div class="col-md-3"><strong>Explore</strong><p class="mt-2 mb-1"><a href="menu.html">Our menu</a></p><p class="mb-1"><a href="about.html">Our story</a></p><p><a href="custom-order.html">Custom cake orders</a></p></div>
          <div class="col-md-3"><strong>Visit / Contact</strong><p class="mt-2 mb-1">Alanaganullur,Madurai-01</p><p class="mb-1">+91 6382142325</p><p class="mb-0">Mon–Sat · 9 AM–7 PM</p></div>
        </div>
        <div class="footer-bottom d-flex flex-column flex-sm-row justify-content-between gap-2"><span>© <span id="year"></span> Sweet Crumbs Bakery</span><span>Made with care, served with sweetness.</span></div>
      </div>
    </footer>`;
  const year = document.getElementById("year");
  if (year) year.textContent = new Date().getFullYear();
}

// Demo custom cake form: validates fields and shows a local summary.
// It does not transmit or persist personal information.
const orderForm = document.getElementById("cakeOrderForm");

if (orderForm) {
  orderForm.addEventListener("submit", function (event) {
    event.preventDefault();

    if (!orderForm.checkValidity()) {
      orderForm.reportValidity();
      return;
    }

    const formData = new FormData(orderForm);

    const details = [
      ["Customer Name", formData.get("customerName")],
      ["Phone Number", formData.get("phone")],
      ["Email", formData.get("email") || "Not provided"],
      ["Occasion", formData.get("occasion")],
      ["Cake Flavour", formData.get("flavour")],
      ["Cake Size", formData.get("size")],
      ["Required Date", formData.get("date")],
      ["Budget", formData.get("budget") || "Not specified"],
      ["Preferred Colours", formData.get("colour") || "Not specified"],
      ["Design / Message", formData.get("design") || "Not specified"]
    ];

    const overview = details
      .map(([label, value]) => `${label}: ${value}`)
      .join("\n");

    alert(
      "🎉 Cake Order Request Submitted Successfully!\n\n" +
      "YOUR ORDER PREFERENCES\n" +
      "────────────────────\n" +
      overview +
      "\n\nThank you for choosing Sweet Crumbs Bakery! ❤️"
    );

    orderForm.reset();
  });
}
