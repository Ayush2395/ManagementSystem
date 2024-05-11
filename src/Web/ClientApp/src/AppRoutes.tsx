import React from "react";
import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import UserList from "./components/UserList.tsx";
import Edit from "./components/Edit.tsx"

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: "/users",
    element: <UserList />
  },
  {
    path: "/edit/:uid",
    element: <Edit buttonLabel="Update user" title="Edit user details" />
  },
  {
    path: "/add-user",
    element: <Edit buttonLabel="Create user" title="Add new user" />
  }
];

export default AppRoutes;
