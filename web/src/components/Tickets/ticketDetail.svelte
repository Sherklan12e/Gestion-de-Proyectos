<script>
  import { onMount } from 'svelte';

  let ticket = null;
  let ticketId = '';
  let loading = true;
  let error = null;

  onMount(async () => {
    try {
      // Obtener el ID del ticket de la URL
      ticketId = window.location.pathname.split('/').pop();
      const response = await fetch(`http://localhost:5180/api/tickets/${ticketId}`);
      if (!response.ok) throw new Error('Error al cargar el ticket');
      const data = await response.json();
      ticket = data;
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
  {:else if ticket}
    <div class="bg-white shadow rounded-lg">
      <!-- Encabezado del Ticket -->
      <div class="px-6 py-5 border-b border-gray-200">
        <div class="flex justify-between items-start">
          <div>
            <h1 class="text-2xl font-bold text-gray-900">{ticket.nombre}</h1>
            <div class="mt-2 flex items-center space-x-4">
              <span class={`px-3 py-1 rounded-full text-sm font-medium ${getStatusColor(ticket.estado)}`}>
                {ticket.estado}
              </span>
              <span class="text-gray-500">Creado el {new Date(ticket.fechaCreacion).toLocaleDateString()}</span>
            </div>
          </div>
        </div>
      </div>

      <!-- Contenido del Ticket -->
      <div class="px-6 py-5">
        <!-- Descripción -->
        <div class="mb-8">
          <h2 class="text-lg font-medium mb-2">Descripción</h2>
          <p class="text-gray-600">{ticket.descripcion}</p>
        </div>

        <!-- Detalles -->
        <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
          <!-- Fechas -->
          <div class="bg-gray-50 p-4 rounded-lg">
            <h3 class="font-medium mb-3">Fechas</h3>
            <div class="space-y-2">
              <div>
                <span class="text-gray-500">Fecha Inicio:</span>
                <span>{ticket.fechaInicio ? new Date(ticket.fechaInicio).toLocaleDateString() : 'No establecida'}</span>
              </div>
              <div>
                <span class="text-gray-500">Fecha Fin:</span>
                <span>{ticket.fechaFin ? new Date(ticket.fechaFin).toLocaleDateString() : 'No establecida'}</span>
              </div>
            </div>
          </div>

          <!-- Asignación -->
          <div class="bg-gray-50 p-4 rounded-lg">
            <h3 class="font-medium mb-3">Asignado a</h3>
            {#if ticket.usuarioAsignado}
              <div class="flex items-center space-x-3">
                <div class="w-8 h-8 bg-blue-100 rounded-full flex items-center justify-center">
                  {ticket.usuarioAsignado.nombre[0].toUpperCase()}
                </div>
                <div>
                  <p class="font-medium">{ticket.usuarioAsignado.nombre}</p>
                  <p class="text-sm text-gray-500">{ticket.usuarioAsignado.email}</p>
                </div>
              </div>
            {:else}
              <p class="text-gray-500">No asignado</p>
            {/if}
          </div>
        </div>

        <!-- Comentarios si los hay -->
        {#if ticket.comentarios && ticket.comentarios.length > 0}
          <div class="mt-8">
            <h2 class="text-lg font-medium mb-4">Comentarios</h2>
            <div class="space-y-4">
              {#each ticket.comentarios as comentario}
                <div class="bg-gray-50 p-4 rounded-lg">
                  <div class="flex justify-between items-start">
                    <div>
                      <p class="font-medium">{comentario.usuario?.nombre || 'Usuario'}</p>
                      <p class="text-sm text-gray-500">{new Date(comentario.fechaCreacion).toLocaleString()}</p>
                    </div>
                  </div>
                  <p class="mt-2">{comentario.contenido}</p>
                </div>
              {/each}
            </div>
          </div>
        {/if}
      </div>
    </div>
  {/if}
</div> 