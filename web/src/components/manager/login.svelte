<script>
  import { onMount } from 'svelte';
  import { navigate } from 'svelte-routing';
  import { auth } from './authStore';
  
  let email = "";
  let password = "";
  let error = "";
  let loading = false;

  // Verificar si ya está logueado al cargar el componente
  onMount(() => {
    if (auth.checkAuth()) {
      navigate('/projects');
    }
  });

  async function handleLogin(e) {
    e.preventDefault();
    loading = true;
    error = "";

    try {
      const loginData = {
        email: email,
        password: password
      };

      const response = await fetch('http://localhost:5180/api/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(loginData)
      });

      const result = await response.json();

      if (result === true) {
        // Guardar estado de autenticación
        auth.login({ email }); // Puedes guardar más datos del usuario si los necesitas
        navigate('/projects');
      } else {
        throw new Error('Credenciales incorrectas');
      }
      
    } catch (err) {
      console.error('Error en login:', err);
      error = err.message || 'Error al iniciar sesión';
    } finally {
      loading = false;
    }
  }
</script>

<div class="min-h-screen flex items-center justify-center bg-gray-50">
  <div class="max-w-md w-full space-y-8 p-8 bg-white rounded-lg shadow-md">
    <h2 class="text-center text-3xl font-bold">Iniciar Sesión</h2>
    
    {#if error}
      <div class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative" role="alert">
        <span class="block sm:inline">{error}</span>
      </div>
    {/if}

    <form class="mt-8 space-y-6" on:submit={handleLogin}>
      <div class="rounded-md shadow-sm space-y-4">
        <div>
          <label for="email" class="sr-only">Correo electrónico</label>
          <input
            id="email"
            bind:value={email}
            type="email"
            required
            class="appearance-none rounded-lg relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-blue-500 focus:border-blue-500"
            placeholder="Correo electrónico"
          />
        </div>
        <div>
          <label for="password" class="sr-only">Contraseña</label>
          <input
            id="password"
            bind:value={password}
            type="password"
            required
            class="appearance-none rounded-lg relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-blue-500 focus:border-blue-500"
            placeholder="Contraseña"
          />
        </div>
      </div>

      <div class="flex items-center justify-between">
        <div class="text-sm">
          <a href="/register" class="font-medium text-blue-600 hover:text-blue-500">
            ¿No tienes cuenta? Regístrate
          </a>
        </div>
      </div>

      <button
        type="submit"
        disabled={loading}
        class="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:opacity-50 disabled:cursor-not-allowed"
      >
        {#if loading}
          <svg class="animate-spin -ml-1 mr-3 h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
            <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
            <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
          </svg>
        {/if}
        {loading ? 'Iniciando sesión...' : 'Iniciar Sesión'}
      </button>
    </form>
  </div>
</div>
