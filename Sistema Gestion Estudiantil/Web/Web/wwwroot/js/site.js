document.addEventListener("DOMContentLoaded", function () {
    // Aplica una clase para la animación de entrada inicial del body
    document.body.classList.add("page-enter");

    // Obtener elementos del menú lateral
    const menuToggle = document.getElementById("menuToggle");
    const menuClose = document.getElementById("menuClose");
    const sidebarMenu = document.getElementById("sidebarMenu");
    const sidebarOverlay = document.getElementById("sidebarOverlay");

    // Función para abrir el sidebar
    function openSidebar() {
        sidebarMenu.style.transform = "translateX(0)"; // Muestra el sidebar
        if (sidebarOverlay) sidebarOverlay.classList.add("active"); // Muestra fondo oscuro
        sidebarMenu.setAttribute("tabindex", "-1");
        sidebarMenu.focus(); // Da foco al sidebar para accesibilidad
    }

    // Función para cerrar el sidebar
    function closeSidebar() {
        sidebarMenu.style.transform = "translateX(100%)"; // Oculta el sidebar
        if (sidebarOverlay) sidebarOverlay.classList.remove("active"); // Oculta fondo oscuro
    }

    // Solo agregar eventos si todos los elementos existen
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
