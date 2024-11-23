<script>
  import { onMount } from 'svelte';
  
  let projectData = {
    nombre: '',
    descripcion: '',
    creacionUsuario: ''
  };

  let loggedInEmail = '';
  
  onMount(async () => {
    // Obtener email del usuario logueado desde la cache
    const cachedUser = JSON.parse(localStorage.getItem('auth'));
    console.log(cachedUser);
    if (cachedUser?.user?.email) {
      loggedInEmail = cachedUser.user.email;
    
      try {
        const response = await fetch('http://localhost:5180/api');
        const users = await response.json();
        
        const matchingUser = users.find(user => user.email === loggedInEmail);
        if (matchingUser) {
          projectData.creacionUsuario = matchingUser.id;
          console.log('Usuario encontrado:', matchingUser.id);
        }
      } catch (error) {
        console.error('Error al obtener usuarios:', error);
      }
    }
  });

  async function handleCreateProject(e) {
    e.preventDefault();
    try {
      const response = await fetch('http://localhost:5180/api/proyecto', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(projectData)
      });

      if (!response.ok) {
        throw new Error('Error al crear el proyecto');
      }

      const text = await response.text();
      if (text) {
        const result = JSON.parse(text);
        console.log('Proyecto creado:', result);
      } else {
        console.log('Proyecto creado exitosamente');
      }
      
      // Redirigir a la página de proyectos
      window.location.href = '/projects';
      // Opcional: Limpiar el formulario después de crear
      projectData = {
        nombre: '',
        descripcion: '',
        creacionUsuario: projectData.creacionUsuario // Mantener el usuario
      };
      
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
          bind:value={projectData.nombre}
          required
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
        />
      </div>

      <div>
        <label class="block text-sm font-medium text-gray-700">Descripción</label>
        <textarea
          bind:value={projectData.descripcion}
          rows="4"
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
        ></textarea>
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
