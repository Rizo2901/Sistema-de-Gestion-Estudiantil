document.addEventListener("DOMContentLoaded", function () {
    const menuToggle = document.getElementById("menuToggle");
    const menuClose = document.getElementById("menuClose");
    const sidebarMenu = document.getElementById("sidebarMenu");
    const sidebarOverlay = document.getElementById("sidebarOverlay");

    const toggleBtn = document.getElementById("toggleSidebarBtn");
    const fixedBtn = document.getElementById("toggleSidebarBtnFixed");
    const submenu = document.getElementById("submenuDashboard");
    const toggleIcon = document.getElementById("toggleIcon");

    const openSidebar = () => {
        if (!sidebarMenu) return;
        sidebarMenu.style.transform = "translateX(0)";
        sidebarMenu.setAttribute("tabindex", "-1");
        sidebarMenu.focus();
        if (sidebarOverlay) sidebarOverlay.classList.add("active");
    };

    const closeSidebar = () => {
        if (!sidebarMenu) return;
        sidebarMenu.style.transform = "translateX(100%)";
        if (sidebarOverlay) sidebarOverlay.classList.remove("active");
    };

    if (menuToggle) menuToggle.addEventListener("click", openSidebar);
    if (menuClose) menuClose.addEventListener("click", closeSidebar);
    if (sidebarOverlay) sidebarOverlay.addEventListener("click", closeSidebar);

    function toggleSubmenu() {
        if (!submenu) return;

        const isHidden = window.getComputedStyle(submenu).display === "none";

        if (isHidden) {
            submenu.style.display = "block";
            if (toggleIcon) {
                toggleIcon.classList.remove("bi-chevron-right");
                toggleIcon.classList.add("bi-chevron-left");
            }
            if (fixedBtn) fixedBtn.style.display = "none";
        } else {
            submenu.style.display = "none";
            if (toggleIcon) {
                toggleIcon.classList.remove("bi-chevron-left");
                toggleIcon.classList.add("bi-chevron-right");
            }
            if (fixedBtn) fixedBtn.style.display = "inline-block";
        }
    }

    if (toggleBtn) toggleBtn.addEventListener("click", toggleSubmenu);
    if (fixedBtn) fixedBtn.addEventListener("click", toggleSubmenu);

    // Estado inicial: menú visible, botón fijo oculto, icono izquierda
    if (submenu && fixedBtn) {
        submenu.style.display = "block";
        fixedBtn.style.display = "none";
        if (toggleIcon) {
            toggleIcon.classList.remove("bi-chevron-right");
            toggleIcon.classList.add("bi-chevron-left");
        }
    }

    // Animaciones IntersectionObserver (opcional)
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add("animate__animated", "animate__fadeInUp");
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.2 });

    document.querySelectorAll(".wow").forEach(el => observer.observe(el));
});

window.addEventListener("load", () => {
    document.body.classList.add("page-enter");
});

const carouselElement = document.querySelector('#heroCarousel');
if (carouselElement && typeof bootstrap !== "undefined") {
    new bootstrap.Carousel(carouselElement, {
        interval: 3500,
        ride: 'carousel'
    });
}
