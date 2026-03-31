import { createBrowserRouter } from 'react-router';
import { Root } from './Root';
import { Home } from './pages/Home';
import { RecipeDetail } from './pages/RecipeDetail';
import { Recipes } from './pages/Recipes';
import { Categories } from './pages/Categories';
import { Tips } from './pages/Tips';
import { Community } from './pages/Community';
import { MealPlanner } from './pages/MealPlanner';

export const router = createBrowserRouter([
  {
    path: '/',
    Component: Root,
    children: [
      { index: true, Component: Home },
      { path: 'recipes', Component: Recipes },
      { path: 'recipe/:id', Component: RecipeDetail },
      { path: 'categories', Component: Categories },
      { path: 'tips', Component: Tips },
      { path: 'community', Component: Community },
      { path: 'meal-planner', Component: MealPlanner },
    ],
  },
]);
