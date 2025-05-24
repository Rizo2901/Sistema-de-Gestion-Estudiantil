document.addEventListener("DOMContentLoaded", function () {
    document.body.classList.add("page-enter");

    const menuToggle = document.getElementById("menuToggle");
    const menuClose = document.getElementById("menuClose");
    const sidebarMenu = document.getElementById("sidebarMenu");
    const sidebarOverlay = document.getElementById("sidebarOverlay");

    function openSidebar() {
        sidebarMenu.style.transform = "translateX(0)";
        if (sidebarOverlay) sidebarOverlay.classList.add("active");
        sidebarMenu.setAttribute("tabindex", "-1");
        sidebarMenu.focus();
    }

    function closeSidebar() {
        sidebarMenu.style.transform = "translateX(100%)";
        if (sidebarOverlay) sidebarOverlay.classList.remove("active");
    }

    if (menuToggle && menuClose && sidebarMenu) {
        menuToggle.addEventListener("click", openSidebar);
        menuClose.addEventListener("click", closeSidebar);
        if (sidebarOverlay) {
            sidebarOverlay.addEventListener("click", closeSidebar);
        }
    }

    // Animaciones WOW
    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add("animate__animated", "animate__fadeInUp");
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.2 });

    document.querySelectorAll(".wow").forEach(el => observer.observe(el));
});

window.addEventListener("load", function () {
    document.body.classList.add("page-enter");
});

const carouselElement = document.querySelector('#heroCarousel');
if (carouselElement) {
    const carousel = new bootstrap.Carousel(carouselElement, {
        interval: 3500,
        ride: true
    });
}
