import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import Home from "./pages/Home.jsx";
import Login from "./pages/Login.jsx";
import Layout from "./components/Layout.jsx";

function App() {
  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path="/" element={<Layout />}>
        <Route index element={<Home />} />
        <Route path="login" element={<Login />} />
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

// const RandomComonent = (props) => {
//   console.log(props.randomId);
//   return <div>{props.children}</div>;
// };

// const RandomComonent2 = () => {
//   return (
//     <RandomComonent randomId={2}>
//       <div>2</div>
//     </RandomComonent>
//   );
// };
