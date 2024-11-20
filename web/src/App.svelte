<script>
  import { Router, Link, Route } from "svelte-routing";
  import Navbar from './components/layouts/Navbar.svelte';
  import Footer from './components/layouts/Footer.svelte';
  
  // Importa tus componentes
  import Projects from './components/Projects/projectos.svelte';
  import CreateProject from './components/Projects/Crearprojecto.svelte';
  import Login from './components/manager/login.svelte';
  import Profile from './components/manager/profile.svelte';
  import Edit from './components/manager/editar.svelte';
  import Admins from './components/manager/admins.svelte';

  export let url = "";
</script>

<Router {url}>
  <main>
    <Navbar />
    
    <div class="min-h-screen container mx-auto px-4 py-8">
      <Route path="/" component={Projects} />
      <Route path="/projects/new" component={CreateProject} />
      <Route path="/login" component={Login} />
      <Route path="/profile" component={Profile} />
      <Route path="/edit/:id" component={Edit} />
      <Route path="/admins" component={Admins} />
    </div>
    
    <Footer />
  </main>
</Router>