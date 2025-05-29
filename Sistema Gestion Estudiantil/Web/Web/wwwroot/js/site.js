document.addEventListener("DOMContentLoaded", function () {
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


    // Animaciones "WOW" con IntersectionObserver
    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                // Cuando el elemento entra en pantalla, añade animación
                entry.target.classList.add("animate__animated", "animate__fadeInUp");
                observer.unobserve(entry.target); // Solo animar una vez
            }
        });
    }, { threshold: 0.2 });

    // Aplicar el observer a todos los elementos con clase .wow
    document.querySelectorAll(".wow").forEach(el => observer.observe(el));
});

// Evento cuando la página ya está completamente cargada
window.addEventListener("load", function () {
    document.body.classList.add("page-enter"); // Vuelve a aplicar animación de entrada
});

// Configurar y activar el carousel de Bootstrap (si existe)
const carouselElement = document.querySelector('#heroCarousel');
if (carouselElement) {
    const carousel = new bootstrap.Carousel(carouselElement, {
        interval: 3500, // Cambia de slide cada 3.5 segundos
        ride: 'carousel' // Inicia automáticamente
    });
}
