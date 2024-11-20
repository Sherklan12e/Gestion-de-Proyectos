<script>
    import { onMount } from "svelte";
    import { writable } from "svelte/store";
  
    // Estados para los campos del formulario
    let username = "";
    let email = "";
    let password = "";
    let confirmPassword = "";
    let message = "";
  
    // URL de la API
    const API_URL = "https://tudominio.com/api/register";
  
    // Función para enviar los datos del formulario
    async function registerUser() {
      if (password !== confirmPassword) {
        message = "Las contraseñas no coinciden.";
        return;
      }
  
      try {
        // Configuración del cuerpo de la solicitud
        const response = await fetch(API_URL, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ username, email, password }),
        });
  
        const data = await response.json();
  
        if (response.ok) {
          message = "Registro exitoso. ¡Bienvenido!";
          // Limpia los campos
          username = email = password = confirmPassword = "";
        } else {
          message = data.message || "Error al registrar el usuario.";
        }
      } catch (error) {
        message = "Ocurrió un error al comunicarse con la API.";
        console.error(error);
      }
    }
  </script>
  
  <style>
    .form-container {
      max-width: 400px;
      margin: auto;
      padding: 20px;
      border: 1px solid #ccc;
      border-radius: 5px;
      background-color: #f9f9f9;
    }
    .error {
      color: red;
    }
    .success {
      color: green;
    }
  </style>
  
  <div class="form-container">
    <h2>Registro</h2>
    <form on:submit|preventDefault={registerUser}>
      <div>
        <label for="username">Usuario:</label>
        <input
          id="username"
          type="text"
          bind:value={username}
          placeholder="Ingresa tu usuario"
          required
        />
      </div>
      <div>
        <label for="email">Correo Electrónico:</label>
        <input
          id="email"
          type="email"
          bind:value={email}
          placeholder="Ingresa tu correo"
          required
        />
      </div>
      <div>
        <label for="password">Contraseña:</label>
        <input
          id="password"
          type="password"
          bind:value={password}
          placeholder="Ingresa tu contraseña"
          required
        />
      </div>
      <div>
        <label for="confirmPassword">Confirmar Contraseña:</label>
        <input
          id="confirmPassword"
          type="password"
          bind:value={confirmPassword}
          placeholder="Confirma tu contraseña"
          required
        />
      </div>
      <button type="submit">Registrarse</button>
    </form>
    {#if message}
      <p class={message.includes("exitoso") ? "success" : "error"}>{message}</p>
    {/if}
  </div>
  