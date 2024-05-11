import { configureStore } from "@reduxjs/toolkit";
import userlistSlice from "./features/userlist-slice.ts";
import { TypedUseSelectorHook, useSelector } from "react-redux";

export const store = configureStore({
    reducer: { userlistSlice }
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export const useAppState: TypedUseSelectorHook<RootState> = useSelector
