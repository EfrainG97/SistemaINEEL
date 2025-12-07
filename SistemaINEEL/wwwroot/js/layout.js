
(function () {
    'use strict';

    if (window.layoutJsInitialized) {
        console.log('Layout JavaScript ya estaba inicializado - evitando duplicación');
        return;
    }
    window.layoutJsInitialized = true;

    // Navbar scroll effect
    function initNavbarScroll() {
        const navbar = document.querySelector('.navbar-custom');
        
        window.addEventListener('scroll', function () {
            if (window.scrollY > 20) {
                navbar.classList.add('scrolled');
            } else {
                navbar.classList.remove('scrolled');
            }
        }, { passive: true });
    }

    // Scroll to top button
    function initScrollToTop() {
        const scrollBtn = document.getElementById('scrollTopBtn');
        
        if (!scrollBtn) return;

        // Show/hide button based on scroll position
        window.addEventListener('scroll', function () {
            if (window.scrollY > 300) {
                scrollBtn.classList.add('show');
            } else {
                scrollBtn.classList.remove('show');
            }
        }, { passive: true });

        // Scroll to top on click
        scrollBtn.addEventListener('click', function () {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });
    }

    // Active nav link highlighting
    function initActiveNavLink() {
        const currentPath = window.location.pathname;
        const navLinks = document.querySelectorAll('.navbar-nav .nav-link');

        navLinks.forEach(link => {
            const href = link.getAttribute('href');
            
            if (href && (currentPath === href || currentPath.startsWith(href + '/'))) {
                link.classList.add('active');
            } else {
                link.classList.remove('active');
            }
        });
    }

    // Auto-close mobile navbar on link click
    function initMobileNavClose() {
        const navLinks = document.querySelectorAll('.navbar-nav .nav-link');
        const navbarCollapse = document.querySelector('.navbar-collapse');

        if (!navbarCollapse) return;

        navLinks.forEach(link => {
            link.addEventListener('click', function () {
                if (window.innerWidth < 992) {
                    const bsCollapse = bootstrap.Collapse.getInstance(navbarCollapse);
                    if (bsCollapse) {
                        bsCollapse.hide();
                    }
                }
            });
        });
    }

    function initLogoutConfirmation() {
        document.body.addEventListener('click', function(e) {
            const logoutLink = e.target.closest('.logout-item');
            
            if (logoutLink) {
                e.preventDefault();
                e.stopImmediatePropagation();
                
                if (confirm('Estas seguro de que deseas cerrar sesion?')) {
                    window.location.href = logoutLink.href;
                }
            }
        }, { capture: true }); 
    }

    // Dropdown hover effect (desktop only) - Mejorado
    function initDropdownHover() {
        const userDropdown = document.querySelector('.user-dropdown');
        
        if (!userDropdown) return;

        const dropdownToggle = userDropdown.querySelector('.dropdown-toggle');
        const dropdownMenu = userDropdown.querySelector('.dropdown-menu');
        
        if (!dropdownToggle || !dropdownMenu) return;

        let hideTimeout;

        // Mostrar dropdown al pasar el mouse
        userDropdown.addEventListener('mouseenter', function () {
            if (window.innerWidth >= 992) {
                clearTimeout(hideTimeout);
                dropdownToggle.classList.add('show');
                dropdownMenu.classList.add('show');
            }
        });

        // Ocultar dropdown al salir, pero con delay
        userDropdown.addEventListener('mouseleave', function () {
            if (window.innerWidth >= 992) {
                hideTimeout = setTimeout(() => {
                    dropdownToggle.classList.remove('show');
                    dropdownMenu.classList.remove('show');
                }, 300);
            }
        });

        // Mantener visible si el mouse está sobre el menu
        dropdownMenu.addEventListener('mouseenter', function () {
            clearTimeout(hideTimeout);
        });

        dropdownMenu.addEventListener('mouseleave', function () {
            if (window.innerWidth >= 992) {
                hideTimeout = setTimeout(() => {
                    dropdownToggle.classList.remove('show');
                    dropdownMenu.classList.remove('show');
                }, 100);
            }
        });
    }

    // Smooth scroll for anchor links
    function initSmoothScroll() {
        document.querySelectorAll('a[href^="#"]').forEach(anchor => {
            anchor.addEventListener('click', function (e) {
                const href = this.getAttribute('href');
                
                if (href !== '#' && href !== '') {
                    e.preventDefault();
                    const target = document.querySelector(href);
                    
                    if (target) {
                        target.scrollIntoView({
                            behavior: 'smooth',
                            block: 'start'
                        });
                    }
                }
            });
        });
    }

    // Check session periodically
    function initSessionCheck() {
        // Only check if user is logged in
        if (document.querySelector('.user-menu')) {
            setInterval(function () {
                fetch(window.location.href, { method: 'HEAD' })
                    .then(response => {
                        if (response.redirected) {
                            window.location.href = response.url;
                        }
                    })
                    .catch(error => {
                        console.error('Error verificando sesion:', error);
                    });
            }, 300000); // Check every 5 minutes
        }
    }

    // Initialize all functions when DOM is ready
    document.addEventListener('DOMContentLoaded', function () {
        initNavbarScroll();
        initScrollToTop();
        initActiveNavLink();
        initMobileNavClose();
        initLogoutConfirmation();
        initDropdownHover();
        initSmoothScroll();
        initSessionCheck();

        console.log('Layout JavaScript cargado correctamente');
    });

    // Handle page visibility change
    document.addEventListener('visibilitychange', function () {
        if (!document.hidden) {
            initActiveNavLink();
        }
    });

})();

