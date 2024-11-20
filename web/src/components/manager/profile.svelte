<script>
  let user = {
    name: 'Usuario Ejemplo',
    email: 'usuario@ejemplo.com',
    role: 'Developer',
    joinDate: '2024-01-01'
  };

  function handleUpdateProfile() {
    // Implementa la lógica para actualizar el perfil
    console.log('Actualizando perfil...');
  }
</script>

<div class="max-w-4xl mx-auto p-6">
  <div class="bg-white shadow rounded-lg p-8">
    <h2 class="text-2xl font-bold mb-6">Perfil de Usuario</h2>
    
    <div class="space-y-6">
      <div class="flex items-center space-x-4">
        <div class="w-24 h-24 bg-gray-200 rounded-full">
          <!-- Placeholder para avatar -->
        </div>
        <div>
          <h3 class="text-xl font-semibold">{user.name}</h3>
          <p class="text-gray-600">{user.role}</p>
        </div>
      </div>

      <form class="space-y-4" on:submit|preventDefault={handleUpdateProfile}>
        <div>
          <label class="block text-sm font-medium text-gray-700">Nombre</label>
          <input
            type="text"
            bind:value={user.name}
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
          />
        </div>

        <div>
          <label class="block text-sm font-medium text-gray-700">Email</label>
          <input
            type="email"
            bind:value={user.email}
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
          />
        </div>

        <button
          type="submit"
          class="w-full py-2 px-4 border border-transparent rounded-md shadow-sm text-white bg-blue-600 hover:bg-blue-700"
        >
          Actualizar Perfil
        </button>
      </form>
    </div>
  </div>
</div>
