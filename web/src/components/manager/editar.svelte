<script>
  export let id; // ID del elemento a editar

  let formData = {
    title: '',
    description: '',
    status: 'active'
  };

  async function loadData() {
    // Cargar datos existentes
    try {
      // Implementa la carga de datos según el ID
      console.log('Cargando datos para ID:', id);
    } catch (error) {
      console.error('Error al cargar datos:', error);
    }
  }

  async function handleSubmit(e) {
    e.preventDefault();
    try {
      // Implementa la actualización de datos
      console.log('Actualizando:', formData);
    } catch (error) {
      console.error('Error al actualizar:', error);
    }
  }
</script>

<div class="max-w-4xl mx-auto p-6">
  <div class="bg-white shadow rounded-lg p-8">
    <h2 class="text-2xl font-bold mb-6">Editar</h2>

    <form on:submit={handleSubmit} class="space-y-4">
      <div>
        <label class="block text-sm font-medium text-gray-700">Título</label>
        <input
          type="text"
          bind:value={formData.title}
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
        />
      </div>

      <div>
        <label class="block text-sm font-medium text-gray-700">Descripción</label>
        <textarea
          bind:value={formData.description}
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
          rows="4"
        ></textarea>
      </div>

      <div>
        <label class="block text-sm font-medium text-gray-700">Estado</label>
        <select
          bind:value={formData.status}
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
        >
          <option value="active">Activo</option>
          <option value="inactive">Inactivo</option>
        </select>
      </div>

      <button
        type="submit"
        class="w-full py-2 px-4 border border-transparent rounded-md shadow-sm text-white bg-blue-600 hover:bg-blue-700"
      >
        Guardar Cambios
      </button>
    </form>
  </div>
</div>
