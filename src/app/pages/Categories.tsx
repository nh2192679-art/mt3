import { categories } from '../data/recipes';
import { Link } from 'react-router';
import { ChevronRight } from 'lucide-react';

export function Categories() {
  return (
    <div className="min-h-screen">
      <div className="bg-gradient-to-br from-primary/10 to-secondary/10 py-12">
        <div className="container mx-auto px-4">
          <h1 className="mb-4">Browse by Category</h1>
          <p className="text-muted-foreground">
            Explore recipes organized by meal type and cuisine
          </p>
        </div>
      </div>

      <div className="container mx-auto px-4 py-12">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {categories.map((category) => (
            <Link
              key={category.id}
              to="/recipes"
              className="group bg-card rounded-2xl p-8 hover:shadow-xl transition-all hover:-translate-y-1 border border-border"
            >
              <div className="flex items-center justify-between">
                <div>
                  <div className="text-5xl mb-4">{category.icon}</div>
                  <h3 className="mb-2 group-hover:text-primary transition-colors">
                    {category.name}
                  </h3>
                  <p className="text-muted-foreground">{category.count} recipes</p>
                </div>
                <ChevronRight className="h-6 w-6 text-muted-foreground group-hover:text-primary transition-colors" />
              </div>
            </Link>
          ))}
        </div>
      </div>
    </div>
  );
}
