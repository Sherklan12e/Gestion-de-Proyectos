<script>
  import { onMount } from 'svelte';

  let project = null;
  let projectId = '';
  let loading = true;
  let error = null;

  // Estado para el modal de nuevo ticket
  let showNewTicketModal = false;
  let newTicket = {
    nombre: '',
    descripcion: '',
    usuarioAsignadoId: '',
    proyectoId: ''
  };

  // Estado para el modal de agregar usuario
  let showAddUserModal = false;
  let searchTerm = '';
  let searchResults = [];

  let showAllUsers = false;
  const USERS_LIMIT = 8;

  $: visibleUsers = showAllUsers ? project?.usuarios : project?.usuarios?.slice(0, USERS_LIMIT);
  $: hasMoreUsers = project?.usuarios?.length > USERS_LIMIT;

  onMount(async () => {
    try {
      const response = await fetch(`http://localhost:5180/api/proyectos/${projectId}`);
      if (!response.ok) throw new Error('Error al cargar el proyecto');
      const data = await response.json();
      project = data[0];
      loading = false;
    } catch (err) {
      error = err.message;
      loading = false;
    }
  });

  function getStatusColor(estado) {
    switch (estado?.toLowerCase()) {
      case 'abierto': return 'bg-green-100 text-green-800';
      case 'en progreso': return 'bg-yellow-100 text-yellow-800';
      case 'completado': return 'bg-blue-100 text-blue-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  }

  async function handleNewTicket() {
    try {
      const response = await fetch('http://localhost:5180/api/ticket', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          ...newTicket,
          proyectoId: project.id
        })
      });

      if (!response.ok) throw new Error('Error al crear el ticket');
      
      // Recargar los datos del proyecto
      const projectResponse = await fetch(`http://localhost:5180/api/proyectos/${project.id}`);
      project = await projectResponse.json();
      
      showNewTicketModal = false;
      newTicket = {
        nombre: '',
        descripcion: '',
        usuarioAsignadoId: '',
        proyectoId: ''
      };
    } catch (err) {
      console.error('Error:', err);
    }
  }

  async function searchUsers() {
    try {
      const response = await fetch('http://localhost:5180/api');
      const users = await response.json();
      searchResults = users.filter(user => 
        user.nombre.toLowerCase().includes(searchTerm.toLowerCase()) &&
        !project.usuarios.some(u => u.id === user.id)
      );
    } catch (error) {
      console.error('Error al buscar usuarios:', error);
    }
  }

  async function addUserToProject(userId) {
    try {
      const response = await fetch(`http://localhost:5180/api/proyectos/${project.id}/usuario/${userId}`, {
        method: 'POST'
      });

      if (!response.ok) throw new Error('Error al agregar usuario');

      // Recargar los datos del proyecto
      const projectResponse = await fetch(`http://localhost:5180/api/proyectos/${project.id}`);
      project = await projectResponse.json();
      
      showAddUserModal = false;
      searchTerm = '';
      searchResults = [];
    } catch (error) {
      console.error('Error:', error);
    }
  }
</script>

<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
  {#if loading}
    <div class="flex justify-center items-center h-64">
      <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
    </div>
  {:else if error}
    <div class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
      {error}
    </div>
  {:else if project}
    <!-- Encabezado del Proyecto -->
    <div class="mb-8">
      <div class="flex justify-between items-start">
        <div>
            {console.log(project)}
          <h1 class="text-3xl font-bold text-gray-900">{project.nombre}</h1>
          <p class="mt-2 text-gray-600">{project.descripcion}</p>
        </div>
        <div class="flex space-x-4">
          <button
            class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700"
            on:click={() => showNewTicketModal = true}
          >
            Nuevo Ticket
          </button>
          <button
            class="bg-green-600 text-white px-4 py-2 rounded-md hover:bg-green-700"
            on:click={() => showAddUserModal = true}
          >
            Agregar Usuario
          </button>
        </div>
      </div>
      
      <!-- Información del Proyecto -->
      <div class="mt-6 grid grid-cols-1 md:grid-cols-2 gap-6">
        <!-- Equipo -->
        <div class="bg-white p-6 rounded-lg shadow">
          <div class="flex justify-between items-center mb-4">
            <h2 class="text-xl font-semibold">Equipo</h2>
            {#if hasMoreUsers}
              <button 
                class="text-blue-600 hover:text-blue-800 flex items-center space-x-1"
                on:click={() => showAllUsers = !showAllUsers}
              >
                <span>{showAllUsers ? 'Ver menos' : 'Ver más'}</span>
                <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                  {#if showAllUsers}
                    <path fill-rule="evenodd" d="M14.707 12.707a1 1 0 01-1.414 0L10 9.414l-3.293 3.293a1 1 0 01-1.414-1.414l4-4a1 1 0 011.414 0l4 4a1 1 0 010 1.414z" clip-rule="evenodd"/>
                  {:else}
                    <path fill-rule="evenodd" d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z" clip-rule="evenodd"/>
                  {/if}
                </svg>
              </button>
            {/if}
          </div>

          <div class="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-4">
            {#each visibleUsers as usuario}
              <div class="flex flex-col items-center p-4 border rounded-lg hover:shadow-md transition-shadow">
                <div class="w-16 h-16 bg-gradient-to-br from-blue-500 to-purple-500 rounded-lg flex items-center justify-center text-white text-xl font-bold mb-2">
                  {usuario.nombre[0].toUpperCase()}
                </div>
                <div class="text-center">
                  <p class="font-medium truncate w-full">{usuario.nombre}</p>
                  <p class="text-sm text-gray-500 truncate w-full">{usuario.email}</p>
                </div>
              </div>
            {/each}
          </div>
        </div>

        <!-- Estadísticas -->
        <div class="bg-white p-6 rounded-lg shadow">
          <h2 class="text-xl font-semibold mb-4">Estadísticas</h2>
          <div class="grid grid-cols-2 gap-4">
            <div>
              <p class="text-sm text-gray-500">Total Tickets</p>
              <p class="text-2xl font-semibold">{project.tickets?.length || 0}</p>
            </div>
            <div>
              <p class="text-sm text-gray-500">Miembros</p>
              <p class="text-2xl font-semibold">{project.usuarios?.length || 0}</p>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Lista de Tickets -->
    <div class="bg-white shadow rounded-lg">
      <div class="px-4 py-5 sm:px-6">
        <h2 class="text-xl font-semibold">Tickets</h2>
      </div>
      <div class="border-t border-gray-200">
        {#if project.tickets?.length > 0}
          <ul class="divide-y divide-gray-200">
            {#each project.tickets as ticket}
              <li 
                class="px-4 py-4 sm:px-6 hover:bg-gray-50 cursor-pointer transition-colors"
                on:click={() => window.location.href = `/tickets/${ticket.id}`}
              >
                <div class="flex items-center justify-between">
                  <div class="flex-1">
                    <h3 class="text-lg font-medium">{ticket.nombre}</h3>
                    <p class="text-gray-600 mt-1">{ticket.descripcion}</p>
                    <div class="mt-2 flex items-center space-x-2">
                      <span class={`px-2 py-1 text-xs font-medium rounded-full ${getStatusColor(ticket.estado)}`}>
                        {ticket.estado}
                      </span>
                      {#if ticket.fechaInicio}
                        <span class="text-sm text-gray-500">
                          Inicio: {new Date(ticket.fechaInicio).toLocaleDateString()}
                        </span>
                      {/if}
                      {#if ticket.fechaFin}
                        <span class="text-sm text-gray-500">
                          Fin: {new Date(ticket.fechaFin).toLocaleDateString()}
                        </span>
                      {/if}
                    </div>
                  </div>
                </div>
              </li>
            {/each}
          </ul>
        {:else}
          <div class="text-center py-8 text-gray-500">
            No hay tickets creados
          </div>
        {/if}
      </div>
    </div>
  {/if}

  <!-- Modal Nuevo Ticket -->
  {#if showNewTicketModal}
    <div class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
      <div class="bg-white rounded-lg p-6 w-full max-w-md">
        <h2 class="text-xl font-semibold mb-4">Nuevo Ticket</h2>
        <form on:submit|preventDefault={handleNewTicket} class="space-y-4">
          <div>
            <label class="block text-sm font-medium text-gray-700">Nombre</label>
            <input
              type="text"
              bind:value={newTicket.nombre}
              class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
              required
            />
          </div>
          
          <div>
            <label class="block text-sm font-medium text-gray-700">Descripción</label>
            <textarea
              bind:value={newTicket.descripcion}
              class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
              rows="3"
              required
            ></textarea>
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700">Asignar a</label>
            <select
              bind:value={newTicket.usuarioAsignadoId}
              class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
              required
            >
              <option value="">Seleccionar usuario</option>
              {#each project.usuarios as usuario}
                <option value={usuario.id}>{usuario.nombre}</option>
              {/each}
            </select>
          </div>

          <div class="flex justify-end space-x-3 mt-6">
            <button
              type="button"
              class="px-4 py-2 border rounded-md text-gray-700 hover:bg-gray-50"
              on:click={() => showNewTicketModal = false}
            >
              Cancelar
            </button>
            <button
              type="submit"
              class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
            >
              Crear Ticket
            </button>
          </div>
        </form>
      </div>
    </div>
  {/if}

  <!-- Modal Agregar Usuario -->
  {#if showAddUserModal}
    <div class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
      <div class="bg-white rounded-lg p-6 w-full max-w-md">
        <h2 class="text-xl font-semibold mb-4">Agregar Usuario al Proyecto</h2>
        
        <div class="mb-4">
          <input
            type="text"
            placeholder="Buscar usuario por nombre..."
            bind:value={searchTerm}
            on:input={searchUsers}
            class="w-full px-3 py-2 border rounded-md"
          />
        </div>

        <div class="max-h-60 overflow-y-auto">
          {#if searchResults.length > 0}
            <ul class="divide-y divide-gray-200">
              {#each searchResults as user}
                <li class="py-3 flex justify-between items-center">
                  <div>
                    <p class="font-medium">{user.nombre}</p>
                    <p class="text-sm text-gray-500">{user.email}</p>
                  </div>
                  <button
                    class="bg-blue-600 text-white px-3 py-1 rounded-md hover:bg-blue-700"
                    on:click={() => addUserToProject(user.id)}
                  >
                    Agregar
                  </button>
                </li>
              {/each}
            </ul>
          {:else if searchTerm}
            <p class="text-center text-gray-500 py-4">No se encontraron usuarios</p>
          {/if}
        </div>

        <div class="mt-4 flex justify-end">
          <button
            class="px-4 py-2 border rounded-md text-gray-700 hover:bg-gray-50"
            on:click={() => {
              showAddUserModal = false;
              searchTerm = '';
              searchResults = [];
            }}
          >
            Cerrar
          </button>
        </div>
      </div>
    </div>
  {/if}
</div>