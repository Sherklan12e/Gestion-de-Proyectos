<script>
  import { onMount } from "svelte";

  // Lista inicial vacía para los administradores
  let admins = [];

  // Función para cargar los datos desde la API
  async function fetchAdmins() {
    try {
        const response = await fetch("http://localhost:5180/api");
        if (!response.ok) {
        throw new Error("Error al obtener los datos de la API");
      }
      const data = await response.json();

      // Mapear los datos de la API a la estructura necesaria para los administradores
      admins = data.map((admin) => ({
        id: admin.id,
        name: admin.nombre,
        email: admin.email,
        role: "Administrador", // Agregar un rol por defecto, ya que no está en la API
        avatar: `https://api.dicebear.com/7.x/avataaars/svg?seed=${admin.nombre}`, // Generar un avatar usando el nombre
        status: "active", // Estado por defecto, ya que no está en la API
        lastActive: new Date().toISOString(), // Fecha simulada de última actividad
      }));
    } catch (error) {
      console.error("Error al cargar los administradores:", error);
    }
  }

  // Llamar a la función fetchAdmins cuando el componente se monte
  onMount(() => {
    fetchAdmins();
  });

  // Funciones para manejar acciones
  function handleEditAdmin(id) {
    console.log("Editar admin:", id);
  }

  function handleDeleteAdmin(id) {
    console.log("Eliminar admin:", id);
  }

  function handleAddAdmin() {
    console.log("Agregar nuevo admin");
  }
</script>

<div class="max-w-7xl mx-auto p-6">
  <div class="flex justify-between items-center mb-6">
    <h2 class="text-2xl font-bold text-gray-800">Administradores</h2>
    <button
      on:click={handleAddAdmin}
      class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors"
    >
      Agregar Administrador
    </button>
  </div>

  <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
    {#each admins as admin (admin.id)}
      <div class="bg-white rounded-lg shadow-md overflow-hidden">
        <div class="p-6">
          <div class="flex items-center space-x-4">
            <img
              src={admin.avatar}
              alt={admin.name}
              class="w-16 h-16 rounded-full"
            />
            <div>
              <h3 class="text-lg font-semibold text-gray-800">{admin.name}</h3>
              <p class="text-sm text-gray-600">{admin.email}</p>
            </div>
          </div>

          <div class="mt-4 space-y-2">
            <div class="flex justify-between text-sm">
              <span class="text-gray-600">Rol:</span>
              <span class="font-medium">{admin.role}</span>
            </div>

            <div class="flex justify-between text-sm">
              <span class="text-gray-600">Estado:</span>
              <span
                class="px-2 py-1 rounded-full text-xs font-medium"
                class:bg-green-100={admin.status === "active"}
                class:text-green-800={admin.status === "active"}
                class:bg-red-100={admin.status === "inactive"}
                class:text-red-800={admin.status === "inactive"}
              >
                {admin.status === "active" ? "Activo" : "Inactivo"}
              </span>
            </div>

            <div class="flex justify-between text-sm">
              <span class="text-gray-600">Última actividad:</span>
              <span class="text-gray-800"
                >{new Date(admin.lastActive).toLocaleDateString()}</span
              >
            </div>
          </div>

          <div class="mt-6 flex justify-end space-x-2">
            <button
              on:click={() => handleEditAdmin(admin.id)}
              class="px-3 py-1 text-sm text-blue-600 hover:bg-blue-50 rounded-md transition-colors"
            >
              Editar
            </button>
            <button
              on:click={() => handleDeleteAdmin(admin.id)}
              class="px-3 py-1 text-sm text-red-600 hover:bg-red-50 rounded-md transition-colors"
            >
              Eliminar
            </button>
          </div>
        </div>
      </div>
    {/each}
  </div>
</div>

<style>
  /* Estilos personalizados */
</style>
