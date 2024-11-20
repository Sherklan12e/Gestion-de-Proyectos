<script>
    import { onMount } from 'svelte';
    import { navigate } from 'svelte-routing';
    import { auth } from '../manager/authStore';
  
    onMount(() => {
      if (!auth.checkAuth()) {
        navigate('/login');
      }
    });
  </script>
  
  <slot />