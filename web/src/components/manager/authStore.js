import { writable } from 'svelte/store';

// Verificar si hay un estado guardado en localStorage
const storedAuth = localStorage.getItem('auth');
const initialValue = storedAuth ? JSON.parse(storedAuth) : { isLoggedIn: false, user: null };

const authStore = writable(initialValue);

// Exportar funciones helper para manejar el estado
export const auth = {
  subscribe: authStore.subscribe,
  
  login: (userData) => {
    authStore.update(() => ({
      isLoggedIn: true,
      user: userData
    }));
    localStorage.setItem('auth', JSON.stringify({ isLoggedIn: true, user: userData }));
  },
  
  logout: () => {
    authStore.set({ isLoggedIn: false, user: null });
    localStorage.removeItem('auth');
  },
  
  checkAuth: () => {
    const stored = localStorage.getItem('auth');
    if (stored) {
      const data = JSON.parse(stored);
      return data.isLoggedIn;
    }
    return false;
  }
};
