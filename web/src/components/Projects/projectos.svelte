<script>
  import { onMount } from 'svelte';

  let projects = [];
  let loading = true;
  let error = null;

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
    window.location.href = `/projects/${id}/edit`;
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
          class="bg-white rounded-lg shadow-md p-6 cursor-pointer hover:shadow-lg transition-shadow"
          on:click={() => handleProjectClick(project.id)}
        >
          <h3 class="text-xl font-semibold mb-2">{project.nombre}</h3>
          <p class="text-gray-600 mb-4">{project.descripcion}</p>

          <div class="space-y-2">
            <!-- Miembros del equipo -->
            <div class="mt-4">
              <h4 class="text-sm font-medium text-gray-700 mb-2">Equipo:</h4>
              <div class="flex flex-wrap gap-2">
                {#each project.usuarios as usuario}
                  <span class="px-2 py-1 bg-gray-100 rounded-full text-sm">
                    {usuario.nombre}
                  </span>
                {/each}
              </div>
            </div>

            <!-- Fecha de creación -->
            <div class="text-sm text-gray-500 mt-4">
              Creado: {new Date(project.fechaCreacion).toLocaleDateString()}
            </div>

            <div class="flex justify-end mt-4">
              <button
                class="p-2 text-gray-600 hover:text-blue-600"
                on:click|stopPropagation={() => handleEdit(project.id)}
              >
                Editar
              </button>
            </div>
          </div>
        </div>
      {/each}
    </div>
  {/if}
</div>
