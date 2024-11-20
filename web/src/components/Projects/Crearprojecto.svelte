<script>
  let projectData = {
    name: '',
    description: '',
    startDate: '',
    endDate: '',
    status: 'planning',
    team: []
  };

  async function handleCreateProject(e) {
    e.preventDefault();
    try {
      // Implementa la creación del proyecto
      console.log('Creando proyecto:', projectData);
    } catch (error) {
      console.error('Error al crear proyecto:', error);
    }
  }
</script>

<div class="max-w-4xl mx-auto p-6">
  <div class="bg-white shadow rounded-lg p-8">
    <h2 class="text-2xl font-bold mb-6">Crear Nuevo Proyecto</h2>

    <form on:submit={handleCreateProject} class="space-y-6">
      <div>
        <label class="block text-sm font-medium text-gray-700">Nombre del Proyecto</label>
        <input
          type="text"
          bind:value={projectData.name}
          required
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
        />
      </div>

      <div>
        <label class="block text-sm font-medium text-gray-700">Descripción</label>
        <textarea
          bind:value={projectData.description}
          rows="4"
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
        ></textarea>
      </div>

      <div class="grid grid-cols-2 gap-4">
        <div>
          <label class="block text-sm font-medium text-gray-700">Fecha de Inicio</label>
          <input
            type="date"
            bind:value={projectData.startDate}
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
          />
        </div>

        <div>
          <label class="block text-sm font-medium text-gray-700">Fecha de Finalización</label>
          <input
            type="date"
            bind:value={projectData.endDate}
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
          />
        </div>
      </div>

      <div>
        <label class="block text-sm font-medium text-gray-700">Estado</label>
        <select
          bind:value={projectData.status}
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
        >
          <option value="planning">Planificación</option>
          <option value="in-progress">En Progreso</option>
          <option value="completed">Completado</option>
        </select>
      </div>

      <button
        type="submit"
        class="w-full py-2 px-4 border border-transparent rounded-md shadow-sm text-white bg-blue-600 hover:bg-blue-700"
      >
        Crear Proyecto
      </button>
    </form>
  </div>
</div>
