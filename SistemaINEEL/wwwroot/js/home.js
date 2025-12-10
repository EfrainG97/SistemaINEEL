/**
 * Define los módulos disponibles para cada rol del sistema.
 * - Admin: Tiene acceso a Crear, Reportes y Configuraciones.
 * - Usuario: Solo tiene acceso a Crear y Reportes.
 * Este objeto se utiliza para controlar qué tarjetas de módulos se muestran en el dashboard.
 */
const modulosPorRol = {
    admin: [
        {
            id: 'crear',
            icon: 'bi-plus-circle-fill',
            title: 'Crear',
            description: 'Registrar Consecutivo',
            url: '/Acciones/Crear',
            class: 'crear'
        },
        {
            id: 'reportes',
            icon: 'bi-file-earmark-bar-graph-fill',
            title: 'Reportes',
            description: 'Genera reportes y consulta consecutivos',
            url: '/Acciones/Reportes',
            class: 'reportes'
        },
        {
            id: 'configuraciones',
            icon: 'bi-gear-fill',
            title: 'Configuraciones',
            description: 'Administra la configuracion del sistema',
            url: '/Sistema/Config',
            class: 'configuraciones'
        }
    ],
    usuario: [
        {
            id: 'crear',
            icon: 'bi-plus-circle-fill',
            title: 'Crear',
            description: 'Registrar Consecutivo',
            url: '/Acciones/Crear',
            class: 'crear'
        },
        {
            id: 'reportes',
            icon: 'bi-file-earmark-bar-graph-fill',
            title: 'Reportes',
            description: 'Genera reportes y consulta consecutivos',
            url: '/Acciones/Reportes',
            class: 'reportes'
        }
    ]
};

/**
 * Carga dinámicamente los módulos disponibles según el rol del usuario.
 * Los administradores ven todos los módulos (Crear, Reportes, Configuraciones),
 * mientras que los usuarios comunes solo ven Crear y Reportes.
 * El rol se obtiene del atributo data-role del elemento dashboardGrid.
 */
function cargarModulos() {
    if (window.modulosCargados === true) {
        return;
    }
    
    const dashboardGrid = document.getElementById('dashboardGrid');
    
    if (!dashboardGrid) {
        return;
    }
    
    if (dashboardGrid.children.length > 0) {
        window.modulosCargados = true;
        return;
    }

    let userRole = dashboardGrid.getAttribute('data-role');
    
    if (userRole) {
        userRole = String(userRole).toLowerCase().trim().replace(/\s+/g, '').replace(/[^a-z0-9_]/g, '');
    } else {
        userRole = 'usuario';
    }

    let modulos = modulosPorRol[userRole];
    
    if (!modulos) {
        if (userRole && userRole.includes('admin')) {
            modulos = modulosPorRol.admin;
        } else {
            modulos = modulosPorRol.usuario;
        }
    }

    if (modulos && modulos.length > 0) {
        dashboardGrid.innerHTML = '';
        modulos.forEach((modulo) => {
            const card = createModuleCard(modulo);
            dashboardGrid.appendChild(card);
        });
    } else {
        dashboardGrid.innerHTML = '<p>No hay modulos disponibles para tu rol.</p>';
    }
    
    window.modulosCargados = true;
}

(function() {
    function ejecutarCarga() {
        cargarModulos();
    }
    
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', ejecutarCarga);
    } else {
        ejecutarCarga();
    }
    
    window.addEventListener('load', function() {
        setTimeout(ejecutarCarga, 100);
    });
})();

function createModuleCard(modulo) {
    const card = document.createElement('div');
    card.className = `module-card ${modulo.class}`;

    card.innerHTML = `
        <div class="module-icon">
            <i class="bi ${modulo.icon}"></i>
        </div>
        <h3 class="module-title">${modulo.title}</h3>
        <p class="module-description">${modulo.description}</p>
        <a href="${modulo.url}" class="module-btn">
            Acceder
            <i class="bi bi-arrow-right-short"></i>
        </a>
    `;

    card.addEventListener('click', function (e) {
        if (!e.target.closest('.module-btn')) {
            const link = this.querySelector('.module-btn');
            if (link) {
                window.location.href = link.href;
            }
        }
    });

    return card;
}
