<script>
  import { onMount } from 'svelte';
  import 'emoji-picker-element';
  
  let projectData = {
    nombre: '',
    descripcion: '',
    creacionUsuario: ''
  };

  let loggedInEmail = '';
  
  let showEmojiPickerTitle = false;
  let showEmojiPickerDesc = false;
  
  const MAX_CHARS_DESC = 50;  // Máximo de caracteres permitidos para descripción
  const MIN_CHARS_DESC = 20;  // Mínimo de caracteres requeridos para descripción
  const MAX_CHARS_NAME = 40;  // Máximo de caracteres permitidos para nombre
  let charactersRemaining = MAX_CHARS_DESC;

  function toggleEmojiPickerTitle() {
    showEmojiPickerTitle = !showEmojiPickerTitle;
    showEmojiPickerDesc = false;
  }

  function toggleEmojiPickerDesc() {
    showEmojiPickerDesc = !showEmojiPickerDesc;
    showEmojiPickerTitle = false;
  }

  function onEmojiSelectTitle(e) {
    projectData.nombre += e.detail.unicode;
    showEmojiPickerTitle = false;
  }

  function onEmojiSelectDesc(e) {
    projectData.descripcion += e.detail.unicode;
    showEmojiPickerDesc = false;
  }
  
  function updateCharCount(event) {
    const length = event.target.value.length;
    charactersRemaining = MAX_CHARS_DESC - length;
  }
  
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
      <div class="relative">
        <label class="block text-sm font-medium text-gray-700">Nombre del Proyecto</label>
        <div class="flex items-center">
          <input
            type="text"
            bind:value={projectData.nombre}
            required
            maxlength={MAX_CHARS_NAME}
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
          />
          <button
            type="button"
            class="ml-2 p-2 text-gray-500 hover:text-gray-700"
            on:click={toggleEmojiPickerTitle}
          >
            😊
          </button>
        </div>
        <div class="text-sm text-gray-500">
          <span>{projectData.nombre.length}/{MAX_CHARS_NAME} caracteres</span>
        </div>
        
        {#if showEmojiPickerTitle}
          <div class="absolute z-10 mt-1">
            <emoji-picker
              on:emoji-click={onEmojiSelectTitle}
            ></emoji-picker>
          </div>
        {/if}
      </div>

      <div>
        <label class="block text-sm font-medium text-gray-700">Descripción</label>
        <div class="flex items-start">
          <textarea
            bind:value={projectData.descripcion}
            on:input={updateCharCount}
            required
            maxlength={MAX_CHARS_DESC}
            minlength={MIN_CHARS_DESC}
            rows="4"
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
            placeholder="Describa el proyecto (mínimo 20 caracteres)"
          ></textarea>
          <button
            type="button"
            class="ml-2 p-2 text-gray-500 hover:text-gray-700"
            on:click={toggleEmojiPickerDesc}
          >
            😊
          </button>
        </div>
        
        {#if showEmojiPickerDesc}
          <div class="absolute z-10 mt-1">
            <emoji-picker
              on:emoji-click={onEmojiSelectDesc}
            ></emoji-picker>
          </div>
        {/if}
        
        <div class="text-sm text-gray-500 flex justify-between">
          <span>{projectData.descripcion.length}/{MAX_CHARS_DESC} caracteres</span>
          {#if projectData.descripcion.length < MIN_CHARS_DESC}
            <span class="text-red-500">Mínimo {MIN_CHARS_DESC} caracteres requeridos</span>
          {/if}
        </div>
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

<style>
  emoji-picker {
    --background: white;
    --category-emoji-size: 1.25rem;
  }
</style>
