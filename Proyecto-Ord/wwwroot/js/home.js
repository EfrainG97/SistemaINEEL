// Configuración de módulos por rol
const modulosPorRol = {
    superadmin: [
        {
            id: 'crear',
            icon: 'bi-plus-circle-fill',
            title: 'Crear',
            description: 'Registrar Consecutivo',
            url: '/Acciones/Create',
            class: 'crear'
        },
        {
            id: 'consultar',
            icon: 'bi-search',
            title: 'Consultar',
            description: 'Visualizar listado de Consecutivos',
            url: '/Acciones/Consultar',
            class: 'consultar'
        },
        {
            id: 'reportes',
            icon: 'bi-file-earmark-bar-graph-fill',
            title: 'Reportes',
            description: 'Genera reportes',
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
        },
        {
            id: 'eliminar',
            icon: 'bi-trash-fill',
            title: 'Eliminar',
            description: 'Elimina consecutivos',
            url: '/Acciones/Eliminar',
            class: 'eliminar'
        }
    ],
    admin: [
        {
            id: 'crear',
            icon: 'bi-plus-circle-fill',
            title: 'Crear',
            description: 'Registrar Consecutivo',
            url: '/Acciones/Create',
            class: 'crear'
        },
        {
            id: 'consultar',
            icon: 'bi-search',
            title: 'Consultar',
            description: 'Visualizar listado de Consecutivos',
            url: '/Acciones/Consultar',
            class: 'consultar'
        },
        {
            id: 'reportes',
            icon: 'bi-file-earmark-bar-graph-fill',
            title: 'Reportes',
            description: 'Genera reportes',
            url: '/Acciones/Reportes',
            class: 'reportes'
        },
        {
            id: 'eliminar',
            icon: 'bi-trash-fill',
            title: 'Eliminar',
            description: 'Elimina consecutivos',
            url: '/Acciones/Eliminar',
            class: 'eliminar'
        }
    ],
    usuario: [
        {
            id: 'crear',
            icon: 'bi-plus-circle-fill',
            title: 'Crear',
            description: 'Registrar Consecutivo',
            url: '/Acciones/Create',
            class: 'crear'
        },
        {
            id: 'consultar',
            icon: 'bi-search',
            title: 'Consultar',
            description: 'Visualizar listado de Consecutivos',
            url: '/Acciones/Consultar',
            class: 'consultar'
        },
        {
            id: 'reportes',
            icon: 'bi-file-earmark-bar-graph-fill',
            title: 'Reportes',
            description: 'Genera reportes',
            url: '/Acciones/Reportes',
            class: 'reportes'
        }
    ]
};

// Cargar módulos según el rol del usuario
document.addEventListener('DOMContentLoaded', function () {
    const dashboardGrid = document.getElementById('dashboardGrid');
    const userRole = dashboardGrid.getAttribute('data-role').toLowerCase();

    // Obtener los módulos correspondientes al rol
    const modulos = modulosPorRol[userRole] || modulosPorRol.usuario;

    // Generar las tarjetas de módulos
    modulos.forEach((modulo) => {
        const card = createModuleCard(modulo);
        dashboardGrid.appendChild(card);
    });
});

// Función para crear una tarjeta de módulo
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

    // Efecto de clic en toda la tarjeta
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

setInterval(function () {
    fetch('/Home/Index')
        .then(response => {
            if (response.redirected) {
                window.location.href = response.url;
            }
        })
        .catch(error => {
            console.error('Error verificando sesión:', error);
        });
}, 300000);

console.log('Dashboard cargado correctamente');
