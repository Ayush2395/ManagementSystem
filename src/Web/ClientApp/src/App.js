import React from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes.tsx';
import { Layout } from './components/Layout';
import './custom.css';
import AppProvider from './redux/provider.tsx';

export default function App() {
  return (
    <AppProvider>
      <Layout>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}
        </Routes>
      </Layout>
    </AppProvider>
  );
}

