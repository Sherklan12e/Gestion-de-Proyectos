<script>
  import { auth } from '../manager/authStore';
  import { navigate } from 'svelte-routing';

  function handleLogout() {
    auth.logout();
    navigate('/login');
  }

  // Suscribirse al store para mostrar/ocultar elementos según el estado de autenticación
  let isLoggedIn;
  auth.subscribe(value => {
    isLoggedIn = value.isLoggedIn;
  });
</script>

<nav class="bg-white shadow-lg">
  <div class="max-w-6xl mx-auto px-4">
    <div class="flex justify-between items-center h-16">
      <div class="flex items-center">
        <a href="/projects" class="text-xl font-bold">Logo</a>
      </div>
      
      <div class="hidden md:flex items-center space-x-4">
        <a href="/projects" class="hover:text-blue-600">Proyectos</a>
        <a href="/perfil" class="hover:text-blue-600">perfil</a>
        {#if isLoggedIn}
        <button
          on:click={handleLogout}
          class="text-sm font-medium text-gray-500 hover:text-gray-700"
        >
          Cerrar Sesión
        </button>
      {/if}
      </div>
    </div>
  </div>
  
 
</nav>
