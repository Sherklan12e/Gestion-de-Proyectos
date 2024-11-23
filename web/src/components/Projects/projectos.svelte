<script>
  import { onMount } from 'svelte';

  let projects = [];
  let loading = true;
  let error = null;
  let activeDropdown = null;

  onMount(async () => {
    try {
      const response = await fetch('http://localhost:5180/api/proyectos');
      if (!response.ok) throw new Error('Error al cargar los proyectos');
      projects = await response.json();
    } catch (err) {
      error = err.message;
    } finally {
      loading = false;
    }
  });

  function handleProjectClick(id) {
    window.location.href = `/projects/${id}`;
  }

  function handleEdit(id) {
    if (id) {
      window.location.href = `/editProject/${id}`;
    }
  }

  function toggleDropdown(projectId, event) {
    event.stopPropagation();
    activeDropdown = activeDropdown === projectId ? null : projectId;
  }

  // Cerrar dropdown al hacer click fuera
  function handleClickOutside(event) {
    if (!event.target.closest('.dropdown-menu')) {
      activeDropdown = null;
    }
  }

  // Agregar el event listener global
  if (typeof window !== 'undefined') {
    window.addEventListener('click', handleClickOutside);
  }

  async function handleDelete(id, event) {
    event.stopPropagation();
    
    if (confirm('¿Estás seguro de que deseas eliminar este proyecto?')) {
      try {
        const response = await fetch(`http://localhost:5180/api/proyecto/${id}`, {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json'
          }
        });

        if (!response.ok) {
          throw new Error('Error al eliminar el proyecto');
        }

        // Actualizar la lista de proyectos eliminando el proyecto
        projects = projects.filter(project => project.id !== id);
        
        // Cerrar el dropdown
        activeDropdown = null;
      } catch (err) {
        console.error('Error:', err);
        alert('No se pudo eliminar el proyecto');
      }
    }
  }
</script>

<div class="max-w-6xl mx-auto p-6">
  <div class="flex justify-between items-center mb-6">
    <h2 class="text-2xl font-bold">Proyectos</h2>
    <a
      href="/projects/new"
      class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
    >
      Nuevo Proyecto
    </a>
  </div>

  {#if loading}
    <div class="text-center py-10">
      <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600 mx-auto"></div>
    </div>
  {:else if error}
    <div class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
      {error}
    </div>
  {:else if projects.length === 0}
    <div class="text-center py-10 text-gray-500">
      No hay proyectos disponibles
    </div>
  {:else}
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
      {#each projects as project (project.id)}
        <!-- svelte-ignore a11y_click_events_have_key_events -->
        <!-- svelte-ignore a11y_no_static_element_interactions -->
        <div
          class="relative bg-white rounded-lg shadow-md p-6 cursor-pointer hover:shadow-lg transition-shadow"
          on:click={() => handleProjectClick(project.id)}
        >
          <h3 class="text-xl font-semibold mb-2">{project.nombre}</h3>
          <p class="text-gray-600 mb-4">{project.descripcion}</p>

          <div class="space-y-2">
            <!-- Equipo del Proyecto -->
            <div class="mt-6">
              <h4 class="text-sm font-semibold text-gray-900 mb-3">Miembros del Equipo</h4>
              <div class="flex flex-wrap gap-2 items-center">
                {#each project.usuarios.slice(0, 5) as usuario}
                  <div class="flex items-center bg-white border border-gray-200 rounded-full px-3 py-1.5 shadow-sm hover:shadow-md transition-all duration-200">
                    <!-- Avatar del usuario (puedes reemplazar src con la imagen real del usuario) -->
                    <div class="w-6 h-6 rounded-full bg-indigo-100 flex items-center justify-center mr-2">
                      <span class="text-xs font-medium text-indigo-700">
                        {usuario.nombre[0].toUpperCase()}
                      </span>
                    </div>
                    <span class="text-sm font-medium text-gray-700">
                      {usuario.nombre}
                    </span>
                  </div>
                {/each}
                
                {#if project.usuarios.length > 5}
                  <button 
                    class="flex items-center bg-indigo-50 border border-indigo-100 rounded-full px-3 py-1.5 
                           hover:bg-indigo-100 transition-colors duration-200 group cursor-pointer"
                    title="Ver todos los miembros"
                  >
                    <span class="text-sm font-medium text-indigo-600">
                      +{project.usuarios.length - 5} más
                    </span>
                  </button>
                {/if}
              </div>
            </div>

            <!-- Fecha de creación -->
            <div class="text-sm text-gray-500 mt-4">
              Creado: {new Date(project.fechaCreacion).toLocaleDateString()}
            </div>

            <div class="absolute top-4 right-4">
              <div class="relative">
                <button
                  class="p-1 rounded-full hover:bg-gray-100 transition-colors"
                  on:click={(e) => toggleDropdown(project.id, e)}
                >
                  <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-gray-500" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 5v.01M12 12v.01M12 19v.01M12 6a1 1 0 110-2 1 1 0 010 2zm0 7a1 1 0 110-2 1 1 0 010 2zm0 7a1 1 0 110-2 1 1 0 010 2z" />
                  </svg>
                </button>
                
                {#if activeDropdown === project.id}
                  <div 
                    class="dropdown-menu absolute right-0 mt-2 w-48 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 z-50"
                  >
                    <div class="py-1">
                      <button
                        class="w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 flex items-center"
                        on:click|stopPropagation={() => handleEdit(project.id)}
                      >
                        <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z" />
                        </svg>
                        Editar
                      </button>
                      <button
                        class="w-full text-left px-4 py-2 text-sm text-red-600 hover:bg-gray-100 flex items-center"
                        on:click|stopPropagation={(e) => handleDelete(project.id, e)}
                      >
                        <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                        </svg>
                        Eliminar
                      </button>
                    </div>
                  </div>
                {/if}
              </div>
            </div>
          </div>
        </div>
      {/each}
    </div>
  {/if}
</div>
