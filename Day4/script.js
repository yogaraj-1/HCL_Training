
// ===== PRODUCT DATA =====

const products = [
    { id: 1, name: "Smartphone", price: 15999, quantity: 0 },
    { id: 2, name: "Headphones", price: 2499, quantity: 0 },
    { id: 3, name: "Keyboard", price: 3499, quantity: 0 },
    { id: 4, name: "Wireless Mouse", price: 999, quantity: 0 }
];

const GST_RATE = 0.18;


// ===== HTML ELEMENTS =====

const billItems = document.getElementById("billItems");
const cartCount = document.getElementById("cartCount");

const subtotalElement = document.getElementById("subtotal");
const gstElement = document.getElementById("gst");
const discountElement = document.getElementById("discount");
const totalElement = document.getElementById("total");

const discountInput = document.getElementById("discountInput");

const customerName = document.getElementById("customerName");
const customerPhone = document.getElementById("customerPhone");


// ===== FORMAT CURRENCY =====

function formatCurrency(amount) {
    return amount.toLocaleString("en-IN", {
        style: "currency",
        currency: "INR"
    });
}


// ===== DISPLAY DATE AND BILL NUMBER =====

document.getElementById("billDate").textContent =
    new Date().toLocaleDateString("en-IN");


// ===== GET PRODUCT =====

function getProduct(id) {
    return products.find(product => product.id === Number(id));
}


// ===== UPDATE QUANTITY DISPLAY =====

function updateQuantityDisplay(product) {
    document.getElementById(`qty-${product.id}`).textContent =
        product.quantity;
}


// ===== INCREASE QUANTITY =====

document.querySelectorAll(".plus").forEach(button => {
    button.addEventListener("click", function () {
        const product = getProduct(this.dataset.id);

        product.quantity++;

        updateQuantityDisplay(product);
        updateBill();
    });
});


// ===== DECREASE QUANTITY =====

document.querySelectorAll(".minus").forEach(button => {
    button.addEventListener("click", function () {
        const product = getProduct(this.dataset.id);

        if (product.quantity > 0) {
            product.quantity--;
        }

        updateQuantityDisplay(product);
        updateBill();
    });
});


// ===== ADD TO CART =====

document.querySelectorAll(".add-btn").forEach(button => {
    button.addEventListener("click", function () {
        const product = getProduct(this.dataset.id);

        product.quantity++;

        updateQuantityDisplay(product);
        updateBill();
    });
});


// ===== CALCULATE BILL =====

function updateBill() {

    // Get products with quantity greater than zero
    const selectedProducts = products.filter(
        product => product.quantity > 0
    );

    // Calculate number of individual items
    const totalQuantity = selectedProducts.reduce(
        (sum, product) => sum + product.quantity,
        0
    );

    cartCount.textContent = totalQuantity;


    // Display empty cart message
    if (selectedProducts.length === 0) {
        billItems.innerHTML =
            '<p class="empty-cart">Your cart is empty.</p>';
    } else {

        // Display selected products
        billItems.innerHTML = selectedProducts.map(product => `
            <div class="bill-item">
                <span>${product.name}</span>
                <span>${product.quantity}</span>
                <span>${formatCurrency(
                    product.price * product.quantity
                )}</span>
            </div>
        `).join("");
    }


    // Calculate subtotal
    const subtotal = selectedProducts.reduce(
        (sum, product) =>
            sum + product.price * product.quantity,
        0
    );


    // Calculate discount percentage
    let discountPercentage = Number(discountInput.value);

    if (!Number.isFinite(discountPercentage)) {
        discountPercentage = 0;
    }

    discountPercentage = Math.min(
        100,
        Math.max(0, discountPercentage)
    );

    // Calculate discount on subtotal
    const discountAmount =
        subtotal * discountPercentage / 100;

    // Calculate GST on discounted subtotal
    const taxableAmount = subtotal - discountAmount;
    const gstAmount = taxableAmount * GST_RATE;

    // Calculate final amount
    const total = taxableAmount + gstAmount;


    // Display bill summary
    subtotalElement.textContent = formatCurrency(subtotal);

    gstElement.textContent = formatCurrency(gstAmount);

    discountElement.textContent =
        "−" + formatCurrency(discountAmount);

    totalElement.textContent = formatCurrency(total);
}


// ===== UPDATE BILL WHEN DISCOUNT CHANGES =====

discountInput.addEventListener("input", updateBill);


// ===== GENERATE BILL =====

document.getElementById("generateBill").addEventListener(
    "click",
    function () {

        const selectedProducts = products.filter(
            product => product.quantity > 0
        );

        if (selectedProducts.length === 0) {
            alert("Please add at least one product to the cart.");
            return;
        }

        if (!customerName.value.trim()) {
            alert("Please enter the customer name.");
            customerName.focus();
            return;
        }

        if (!customerPhone.value.trim()) {
            alert("Please enter the customer phone number.");
            customerPhone.focus();
            return;
        }

        const phone = customerPhone.value.trim();

        if (!/^\d{10}$/.test(phone)) {
            alert("Please enter a valid 10-digit phone number.");
            customerPhone.focus();
            return;
        }

        alert(
            "Bill generated successfully!\n\n" +
            "Customer: " + customerName.value.trim() + "\n" +
            "Bill Number: " +
            document.getElementById("billNumber").textContent +
            "\nTotal Amount: " + totalElement.textContent
        );
    }
);


// ===== PRINT BILL =====

document.getElementById("printBill").addEventListener(
    "click",
    function () {

        const selectedProducts = products.filter(
            product => product.quantity > 0
        );

        if (selectedProducts.length === 0) {
            alert("Please add products before printing the bill.");
            return;
        }

        if (!customerName.value.trim()) {
            alert("Please enter the customer name.");
            customerName.focus();
            return;
        }

        if (!customerPhone.value.trim()) {
            alert("Please enter the customer phone number.");
            customerPhone.focus();
            return;
        }

        if (!/^\d{10}$/.test(customerPhone.value.trim())) {
            alert("Please enter a valid 10-digit phone number.");
            customerPhone.focus();
            return;
        }

        window.print();
    }
);


// ===== CLEAR CART =====

document.getElementById("clearBill").addEventListener(
    "click",
    function () {

        const confirmClear = confirm(
            "Are you sure you want to clear the cart?"
        );

        if (!confirmClear) {
            return;
        }

        // Reset product quantities
        products.forEach(product => {
            product.quantity = 0;
            updateQuantityDisplay(product);
        });

        // Reset customer details
        customerName.value = "";
        customerPhone.value = "";

        // Reset discount
        discountInput.value = 0;

        // Refresh bill
        updateBill();
    }
);


// ===== INITIAL BILL =====

updateBill();
