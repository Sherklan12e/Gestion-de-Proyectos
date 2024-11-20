<script>
  let projects = [
    {
      id: 1,
      name: 'Proyecto Ejemplo',
      description: 'Descripción del proyecto',
      status: 'in-progress',
      progress: 65
    }
    // Añade más proyectos según necesites
  ];

  function handleProjectClick(id) {
    // Implementa la navegación al detalle del proyecto
    console.log('Ver proyecto:', id);
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

  <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
    {#each projects as project (project.id)}
      <div
        class="bg-white rounded-lg shadow-md p-6 cursor-pointer hover:shadow-lg transition-shadow"
        on:click={() => handleProjectClick(project.id)}
      >
        <h3 class="text-xl font-semibold mb-2">{project.name}</h3>
        <p class="text-gray-600 mb-4">{project.description}</p>
        
        <div class="space-y-2">
          <div class="flex justify-between text-sm">
            <span>Progreso</span>
            <span>{project.progress}%</span>
          </div>
          <div class="w-full bg-gray-200 rounded-full h-2">
            <div
              class="bg-blue-600 h-2 rounded-full"
              style="width: {project.progress}%"
            ></div>
          </div>
          
          <div class="flex justify-between items-center mt-4">
            <span
              class="px-2 py-1 text-sm rounded-full"
              class:bg-yellow-100={project.status === 'in-progress'}
              class:text-yellow-800={project.status === 'in-progress'}
            >
              {project.status}
            </span>
            
            <div class="flex space-x-2">
              <button
                class="p-2 text-gray-600 hover:text-blue-600"
                on:click|stopPropagation={() => handleEdit(project.id)}
              >
                Editar
              </button>
            </div>
          </div>
        </div>
      </div>
    {/each}
  </div>
</div>
