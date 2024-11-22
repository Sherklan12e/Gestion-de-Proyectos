<script>
  import { Router, Link, Route } from "svelte-routing";
  import ProtectedRoute from './components/auth/ProtectedRoute.svelte';
  import Login from './components/manager/login.svelte';
  import Projects from './components/Projects/projectos.svelte';
  import CreateProject from './components/Projects/Crearprojecto.svelte';
  import Profile from './components/manager/profile.svelte';
  import Edit from './components/manager/editar.svelte';
  import Admins from './components/manager/admins.svelte';
  import Navbar from './components/layouts/Navbar.svelte';
  import Footer from './components/layouts/Footer.svelte';
  import ProjectDetail from './components/Projects/proyectoDetail.svelte';
  export let url = "";
</script>

<Router {url}>
  <Navbar />
  <main>
    <Route path="/login" component={Login} />
    
    <!-- Rutas protegidas -->
    <Route path="/projects">
      <ProtectedRoute>
        <Projects />
      </ProtectedRoute>
    </Route>

    <Route path="/profile">
      <ProtectedRoute>
        <Profile />
      </ProtectedRoute>
    </Route>

    <Route path="/projects/new" component={CreateProject} />
    <Route path="/edit/:id" component={Edit} />
    <Route path="/admins" component={Admins} />
    <Route path="/projects/:id" component={ProjectDetail} />
  </main>
  <Footer />
</Router>