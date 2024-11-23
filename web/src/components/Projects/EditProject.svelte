<script>
  export let projectId;
  import { onMount } from 'svelte';
  let projectData = {
    nombre: '',
    descripcion: '',
    creacionUsuario: ''
  };

  let loggedInEmail = '';

  onMount(async () => {
    if (!projectId) return;
    
    const cachedUser = JSON.parse(localStorage.getItem('auth'));
    if (cachedUser?.user?.email) {
      loggedInEmail = cachedUser.user.email;
      try {
        // Obtener el ID del usuario
        const response = await fetch('http://localhost:5180/api');
        const users = await response.json();
        
        const matchingUser = users.find(user => user.email === loggedInEmail);
        if (matchingUser) {
          projectData.creacionUsuario = matchingUser.id;
        }
        // Cargar datos actuales del proyecto
        const projectResponse = await fetch(`http://localhost:5180/api/proyecto/${projectId}`);
        if (!projectResponse.ok) {
          throw new Error('Error al cargar el proyecto');
        }
        const projectDetails = await projectResponse.json();
        if (projectDetails && projectDetails.length > 0) {
          projectData = {
            ...projectDetails[0],
            creacionUsuario: matchingUser.id
          };
        }
      } catch (error) {
        console.error('Error:', error);
      }
    }
  });

  async function handleSubmit() {
    try {
      const response = await fetch(`http://localhost:5180/api/proyecto/${projectId}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(projectData)
      });

      if (response.ok) {
        alert('Proyecto actualizado exitosamente');
        window.location.href = '/projects';
      }
    } catch (error) {
      console.error('Error al actualizar el proyecto:', error);
    }
  }
</script>

<div class="max-w-2xl mx-auto p-6 bg-white rounded-lg shadow-md">
  <h2 class="text-2xl font-bold text-gray-800 mb-6">Editar Proyecto</h2>

  <form on:submit|preventDefault={handleSubmit} class="space-y-6">
    <div class="space-y-2">
      <label for="nombre" class="block text-sm font-medium text-gray-700">
        Nombre del Proyecto
      </label>
      <input 
        type="text" 
        id="nombre" 
        bind:value={projectData.nombre} 
        required
        class="w-full px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
        placeholder="Ingrese el nombre del proyecto"
      />
    </div>

    <div class="space-y-2">
      <label for="descripcion" class="block text-sm font-medium text-gray-700">
        Descripción
      </label>
      <textarea 
        id="descripcion" 
        bind:value={projectData.descripcion} 
        required
        rows="4"
        class="w-full px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors resize-none"
        placeholder="Describa el proyecto"
      ></textarea>
    </div>

    <div class="flex justify-end space-x-4 pt-4">
      <a 
        href="/projects" 
        class="px-4 py-2 text-sm font-medium text-gray-700 bg-gray-100 rounded-md hover:bg-gray-200 transition-colors"
      >
        Cancelar
      </a>
      <button 
        type="submit"
        class="px-4 py-2 text-sm font-medium text-white bg-blue-600 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 transition-colors"
      >
        Guardar Cambios
      </button>
    </div>
  </form>
</div>

<style>
  :global(body) {
    background-color: #f9fafb;
  }
</style>
