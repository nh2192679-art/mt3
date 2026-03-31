// ChefVN - Site JavaScript

// Auto-dismiss alerts
document.addEventListener("DOMContentLoaded", function () {
    // Auto-dismiss alerts after 4 seconds
    setTimeout(function () {
        document.querySelectorAll(".alert-dismissible").forEach(function (alert) {
            var bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
            bsAlert.close();
        });
    }, 4000);

    // Image preview on file input
    var imageInputs = document.querySelectorAll("input[type=file][accept*=image]");
    imageInputs.forEach(function (input) {
        input.addEventListener("change", function (e) {
            var file = e.target.files[0];
            if (file) {
                var reader = new FileReader();
                reader.onload = function (ev) {
                    var preview = document.getElementById("imagePreview");
                    if (preview) {
                        preview.src = ev.target.result;
                        preview.style.display = "block";
                    }
                };
                reader.readAsDataURL(file);
            }
        });
    });

    // Smooth scroll for anchor links
    document.querySelectorAll("a[href^='#']").forEach(function (anchor) {
        anchor.addEventListener("click", function (e) {
            var target = document.querySelector(this.getAttribute("href"));
            if (target) {
                e.preventDefault();
                target.scrollIntoView({ behavior: "smooth" });
            }
        });
    });
});

