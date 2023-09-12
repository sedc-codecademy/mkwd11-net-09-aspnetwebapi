import Header from "./components/Header";
import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Notes from "./pages/Notes";
import Register from "./pages/Register";
import CreateNote from "./pages/CreateNote";
import UpdateNote from "./pages/UpdateNote";
import Note from "./pages/Note";

function App() {
  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path="/" element={<Layout />}>
        <Route index element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/notes" element={<Notes />} />
        <Route path="/notes/:noteId" element={<Note />} />
        <Route path="/notes/create" element={<CreateNote />} />
        <Route path="/notes/update/:id" element={<UpdateNote />} />
      </Route>
    )
  );

  return (
    <>
      <RouterProvider router={router} />
    </>
  );
}

export default App;
